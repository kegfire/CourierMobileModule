using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Courier.API;
using Courier.API.Enums;
using Courier.API.Models;
using Courier.Model;
using Courier.View;
using GalaSoft.MvvmLight.Command;
using Plugin.Toast;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Credentials = CourierMobile.Core.Core.API.Models.Credentials;

namespace Courier.ViewMoodel
{
	public class DocumentsViewModel : BaseViewModel
	{
		private readonly DocumentFolder _folder;
		private bool _isProcessing;
		private bool _filterEnabled;
		private string _searchingNumber;
		private DateTime _fromDate;
		private DateTime _toDate;
		private List<SelectableData<Document>> _docListWithoutFilter;
		private CancellationTokenSource _cancellationTokenSource;
		private CancellationToken _cancellationToken;
		private bool _isRefreshing;
		private readonly Company _company;
		private bool _rejectCommentVisibility;

		public DocumentsViewModel()
		{
		}

		public DocumentsViewModel(Company company, DocumentFolder folder)
		{
			_company = company;
			_folder = folder;
		}

		public void Init()
		{
			if (Connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				CrossToastPopUp.Current.ShowToastMessage(App.NoInternet);
				return;
			}
			_cancellationTokenSource = new CancellationTokenSource();
			_cancellationToken = _cancellationTokenSource.Token;
			using (App.Container.BeginLifetimeScope())
			{
				var client = App.Container.Resolve<ICustomHttpClient>();
				if (string.IsNullOrEmpty(client.AuthToken))
				{
					try
					{
						var result = client.CourierApiService.Logon(new Credentials { Username = _company.UserLogin, Password = _company.UserPassword }).Result;
						client.AuthToken = result.Token;
					}
					catch (Exception e)
					{
						var message = e.Message;
						if (e.InnerException != null)
						{
							message = e.InnerException.Message;
						}
						if (e.InnerException is ApiException exception)
						{
							var error = exception.GetContentAs<ExceptionDetails>();
							message = error.Message;
						}
						Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.DisplayAlert("Ошибка", message, "Закрыть"));
					}
				}
			}
			if (Documents == null)
			{
				Documents = new ObservableCollection<SelectableData<Document>>();
				_docListWithoutFilter = new List<SelectableData<Document>>();
				LoadDocuments();
			}
		}

		/// <summary>
		/// Заголовок страницы.
		/// </summary>
		public string Title
		{
			get
			{
				switch (_folder)
				{
					case DocumentFolder.OnProcess:
						return "На обработке";
					case DocumentFolder.Processed:
						return "Обработанные";
					default:
						return "";
				}
			}
		}

		/// <summary>
		/// Список документов.
		/// </summary>
		public ObservableCollection<SelectableData<Document>> Documents { get; set; }

		/// <summary>
		/// Признак обновления списка документов.
		/// </summary>
		public bool IsRefreshing
		{
			get => _isRefreshing;
			set
			{
				_isRefreshing = value;
				OnPropertyChanged(nameof(IsRefreshing));
			}
		}

		/// <summary>
		/// Выбранный документ.
		/// </summary>
		public Document SelectedDocument { get; set; }

		/// <summary>
		/// Признак работы гашения документов.
		/// </summary>
		public bool IsProcessing
		{
			get => _isProcessing;
			set
			{
				_isProcessing = value;
				OnPropertyChanged(nameof(IsProcessing));
				OnPropertyChanged(nameof(ButtonVisibility));
			}
		}

		/// <summary>
		/// Видимость кнопки "Принять всё".
		/// </summary>
		public bool ButtonVisibility
		{
			get
			{
				if (_folder == DocumentFolder.Processed)
				{
					return false;
				}

				return !IsProcessing;
			}
		}

		/// <summary>
		/// Видимость панели фильтра.
		/// </summary>
		public bool FilterEnabled
		{
			get => _filterEnabled;
			set
			{
				_filterEnabled = value;
				OnPropertyChanged(nameof(FilterEnabled));
				SetDatePickerValues();
				Application.Current.MainPage.ForceLayout();
			}
		}

		/// <summary>
		/// Причина отклонения.
		/// </summary>
		public string RejectReason { get; set; }

		public bool RejectCommentVisibility
		{
			get => _rejectCommentVisibility;
			set
			{
				_rejectCommentVisibility = value;
				OnPropertyChanged(nameof(RejectCommentVisibility));
				OnPropertyChanged(nameof(FilterPanelVisibility));
				Application.Current.MainPage.ForceLayout();
			}
		}

		public bool FilterPanelVisibility => !RejectCommentVisibility;

		private void SetDatePickerValues()
		{
			if (FilterEnabled && Documents?.Any() == true)
			{
				var firstDoc = Documents.OrderBy(x => x.Data.Date);
				FromDate = firstDoc.FirstOrDefault().Data.Date;
				ToDate = firstDoc.LastOrDefault().Data.Date;
			}
		}

		/// <summary>
		/// Номер искомого документа.
		/// </summary>
		public string SearchingNumber
		{
			get => _searchingNumber;
			set
			{
				_searchingNumber = value;
				var docs = _docListWithoutFilter.Where(x => x.Data.Number != null && x.Data.Number.Contains(_searchingNumber));
				Documents = new ObservableCollection<SelectableData<Document>>(docs);
				OnPropertyChanged(nameof(Documents));
			}
		}

		public DateTime FromDate
		{
			get => _fromDate;
			set
			{
				_fromDate = value;
				var docs = _docListWithoutFilter.Where(x => x.Data.Date >= value && x.Data.Date <= ToDate);
				if (!string.IsNullOrEmpty(_searchingNumber))
				{
					docs = docs.Where(x => x.Data.Number != null && x.Data.Number.Contains(_searchingNumber));
				}
				Documents = new ObservableCollection<SelectableData<Document>>(docs);
				OnPropertyChanged(nameof(FromDate));
				OnPropertyChanged(nameof(Documents));
			}
		}

		public DateTime ToDate
		{
			get => _toDate;
			set
			{
				_toDate = value;
				var docs = _docListWithoutFilter.Where(x => x.Data.Date <= value && x.Data.Date >= FromDate);
				if (!string.IsNullOrEmpty(_searchingNumber))
				{
					docs = docs.Where(x => x.Data.Number != null && x.Data.Number.Contains(_searchingNumber));
				}
				Documents = new ObservableCollection<SelectableData<Document>>(docs);
				OnPropertyChanged(nameof(ToDate));
				OnPropertyChanged(nameof(Documents));
			}
		}

		/// <summary>
		/// Обновить список.
		/// </summary>
		public RelayCommand RefreshCommand => new RelayCommand(LoadDocuments);


		/// <summary>
		/// Открыть страницу документа.
		/// </summary>
		public RelayCommand<SelectableData<Document>> OpenDocumentCommand => new RelayCommand<SelectableData<Document>>(OpenDocument);

		/// <summary>
		/// Команда показа или сокрытия панели фильтра.
		/// </summary>
		public RelayCommand ShowFilterCommand => new RelayCommand(() => FilterEnabled = !FilterEnabled);

		/// <summary>
		/// Показать панель для отклонения.
		/// </summary>
		public RelayCommand ShowRejectPanelCommand => new RelayCommand(() => RejectCommentVisibility = true);

		/// <summary>
		/// Команда принятия всех или выбранных документов.
		/// </summary>
		public RelayCommand AcceptAllCommand => new RelayCommand(AcceptAllDocuments);

		/// <summary>
		/// Команда отклонения всех или выбранных документов.
		/// </summary>
		public RelayCommand RejectAllCommand => new RelayCommand(RejectAllDocuments);

		/// <summary>
		/// Команда закрыть панель отклонения.
		/// </summary>

		public RelayCommand CancelRejectCommand => new RelayCommand(() => RejectCommentVisibility = false);

		/// <summary>
		/// Отклонить документы.
		/// </summary>
		private async void RejectAllDocuments()
		{

			if (string.IsNullOrEmpty(RejectReason))
			{
				await Application.Current.MainPage.DisplayAlert("Ошибка", "Причина отклонения обязательна для заполнения", "Закрыть");
				return;
			}

			if (Connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				CrossToastPopUp.Current.ShowToastMessage(App.NoInternet);
				return;
			}

			IEnumerable<SelectableData<Document>> selectedDocs;
			if (!Documents.Any(x => x.Selected))
			{
				var confirmation = await Application.Current.MainPage.DisplayAlert("Отклонить всe", "Вы не выбрали документы для отклонения. Отклонить всe?", "Да", "Нет");
				Task.Run(() =>
				{
					if (confirmation)
					{
						IsProcessing = true;
						RejectCommentVisibility = false;
						//selectedDocs = Documents.Select(x => x.Data);
						selectedDocs = new List<SelectableData<Document>>(Documents);
						foreach (var doc in selectedDocs)
						{
							try
							{
								Thread.Sleep(1000);
								Device.BeginInvokeOnMainThread(() => Documents.Remove(doc));
							}
							catch (Exception e)
							{
								Console.WriteLine(e);
								throw;
							}

						}
						IsProcessing = false;
						RejectCommentVisibility = true;
					}
				}, _cancellationToken);
			}
			else
			{
				selectedDocs = new List<SelectableData<Document>>(Documents.Where(x => x.Selected));
				Task.Run(() =>
				{
					IsProcessing = true;
					RejectCommentVisibility = false;
					foreach (var doc in selectedDocs)
					{
						try
						{
							Thread.Sleep(1000);
							Device.BeginInvokeOnMainThread(() => Documents.Remove(doc));
						}
						catch (Exception e)
						{
							Console.WriteLine(e);
							throw;
						}
					}

					IsProcessing = false;
					RejectCommentVisibility = true;
				}, _cancellationToken);
			}
		}

		/// <summary>
		/// Получить документы с сервера.
		/// </summary>
		private void LoadDocuments()
		{
			if (Connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				CrossToastPopUp.Current.ShowToastMessage(App.NoInternet);
				return;
			}
			IsRefreshing = true;
			Task.Run(() =>
			{
				using (App.Container.BeginLifetimeScope())
				{
					var client = App.Container.Resolve<ICustomHttpClient>();
					try
					{
						var filter = new ApiDocumentFilter {Folder = _folder};
						var documents = client.CourierApiService.DocumentList(client.AuthToken, filter, _cancellationToken).Result;
						var sortedDocs = documents.OrderByDescending(x => x.Date);
						if (_folder == DocumentFolder.OnProcess)
						{
							IsProcessing = true;
						}
						
						foreach (var doc in sortedDocs)
						{
							_cancellationToken.ThrowIfCancellationRequested();
							var card = client.CourierApiService.Document(client.AuthToken, doc.Id).Result;
							Documents.Add(new SelectableData<Document> {Data = card});
							OnPropertyChanged(nameof(Documents));
						}

						IsProcessing = false;
						_docListWithoutFilter = new List<SelectableData<Document>>(Documents);
					}
					catch (OperationCanceledException)
					{
						// ignored.
					}
					catch (Exception e)
					{
						var message = e.Message;
						if (e.InnerException != null && e.InnerException.Message.Contains("Socket closed"))
						{
							return;
						}
						if (e.InnerException != null)
						{
							message = e.InnerException.Message;
						}
						if (e.InnerException is ApiException exception)
						{
							var error = exception.GetContentAs<ExceptionDetails>();
							message = error.Message;
						}
						Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.DisplayAlert("Ошибка", message, "Закрыть"));
					}
					IsRefreshing = false;
				}
			}, _cancellationToken);
		}

		/// <summary>
		/// Открыть выбранный документ
		/// </summary>
		/// <param name="item"></param>
		private async void OpenDocument(object item)
		{
			if (item is SelectableData<Document> doc)
			{
				if (IsRefreshing)
				{
					_cancellationTokenSource.Cancel();
					_cancellationTokenSource = new CancellationTokenSource();
					_cancellationToken = _cancellationTokenSource.Token;
				}
				
				SelectedDocument = doc.Data;
				IsRefreshing = true;
				await Task.Run(async () =>
				{
					using (App.Container.BeginLifetimeScope())
					{
						try
						{
							var client = App.Container.Resolve<ICustomHttpClient>();
							var result = await client.CourierApiService.GetPdf(client.AuthToken, doc.Data.Id, _cancellationToken);
							var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "temp_" + result.Filename);
							if (!File.Exists(path))
							{
								File.WriteAllBytes(path, result.Content);
							}

							var vm = new DocumentCardViewModel(SelectedDocument, path);
							vm.DeleteDoc += DeleteDocFromList;
							Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.Navigation.PushAsync(new DocumentCardPage { BindingContext = vm }));
						}
						catch (Exception ex)
						{
							// Когда отменили операцию.
							if (ex.InnerException != null && ex.InnerException.Message.Contains("Socket closed"))
							{
								return;
							}
							// Когда не успели получить pdf файл. 
							if (ex.InnerException != null && ex.InnerException.Message.Contains("timeout"))
							{
								OpenDocument(item);
								return;
							}

							var message = ex.Message;
							if (ex.InnerException is ApiException exception)
							{
								var error = exception.GetContentAs<ExceptionDetails>();
								message = error.Message;
							}
							Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.DisplayAlert("Ошибка", message, "Закрыть"));
						}
					}
					IsRefreshing = false;
				}, _cancellationToken);
			}
		}

		/// <summary>
		/// Принять все документы в списке.
		/// </summary>
		private async void AcceptAllDocuments()
		{
			if (Connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				CrossToastPopUp.Current.ShowToastMessage(App.NoInternet);
				return;
			}
			IEnumerable<SelectableData<Document>> selectedDocs;
			if (!Documents.Any(x => x.Selected))
			{
				var confirmation = await Application.Current.MainPage.DisplayAlert("Принять всe", "Вы не выбрали документы для принятия. Принять всe?", "Да", "Нет");
				if (confirmation)
				{
					IsProcessing = true;
					//selectedDocs = Documents.Select(x => x.Data);
					selectedDocs = new List<SelectableData<Document>>(Documents);
					Task.Run(() =>
					{
						foreach (var doc in selectedDocs)
						{
							try
							{
								Thread.Sleep(1000);
								Device.BeginInvokeOnMainThread(() => Documents.Remove(doc));
							}
							catch (Exception e)
							{
								Console.WriteLine(e);
							}

						}
						IsProcessing = false;
					}, _cancellationToken);
				}
			}
			else
			{
				selectedDocs = new List<SelectableData<Document>>(Documents.Where(x => x.Selected));
				IsProcessing = true;
				Task.Run(() =>
				{
					foreach (var doc in selectedDocs)
					{
						try
						{
							Thread.Sleep(1000);
							Device.BeginInvokeOnMainThread(() => Documents.Remove(doc));
						}
						catch (Exception e)
						{
							Console.WriteLine(e);
						}
					}
					IsProcessing = false;
				}, _cancellationToken);
			}
		}

		/// <summary>
		/// Удалить документ из списка после успешного гашения.
		/// </summary>
		/// <param name="sender"> Обработанный документ.</param>
		/// <param name="e"> Агрумент.</param>
		private void DeleteDocFromList(object sender, EventArgs e)
		{
			var doc = (Document)sender;
			var document = Documents.FirstOrDefault(x => x.Data.Id == doc.Id);
			_docListWithoutFilter.Remove(document);
			Documents.Remove(document);
		}


		/// <summary>
		/// Отменить текущие задачи в потоках.
		/// </summary>
		public void OnBackButtonPressed()
		{
			_cancellationTokenSource.Cancel();
		}
	}
}