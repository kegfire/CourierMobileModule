using System;
using System.Threading;
using System.Threading.Tasks;
using Courier.API.Models;
using CourierMobile.Core.Core.API.Enums;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Courier.ViewMoodel
{
	public class DocumentCardViewModel : BaseViewModel
	{
		private readonly Document _document;

		private readonly Actions _actionForDocument;
		private bool _isProcessing;

		public DocumentCardViewModel() { }

		public DocumentCardViewModel(Document document, string pdfPath)
		{
			_document = document;
			Uri = pdfPath;
			_actionForDocument = GetAction();
		}

		/// <summary>
		/// Получить действие, которое доступно для обработки документа.
		/// </summary>
		/// <returns> Доступное действие.</returns>
		private Actions GetAction()
		{
			if (_document.Actions.HasFlag(Actions.AcceptWithTicket))
			{
				return Actions.AcceptWithTicket;
			}

			if (_document.Actions.HasFlag(Actions.Accept))
			{
				return Actions.Accept;
			}

			if (_document.Actions.HasFlag(Actions.Sign))
			{
				return Actions.Sign;
			}

			if (_document.Actions.HasFlag(Actions.Send))
			{
				return Actions.Send;
			}

			return Actions.None;
		}

		/// <summary>
		/// Путь к сохраненной pdf-ке.
		/// </summary>
		public string Uri { get; }

		/// <summary>
		/// Заголовок страницы.
		/// </summary>
		public string Title => $"№{_document?.Number} от {_document?.Date}";

		/// <summary>
		/// Текст кнопки для принятия документа.
		/// </summary>
		public string AcceptButtonText
		{
			get
			{
				switch (_actionForDocument)
				{
					case Actions.AcceptWithTicket:
					case Actions.Accept:
						return "Принять";
					case Actions.Sign:
						return "Подписать";
					case Actions.Send:
						return "Отправить";
					default:
						return string.Empty;
				}
			}
		}

		/// <summary>
		/// Видимость кнопки на принятие.
		/// </summary>
		public bool VisibilityAcceptButton => !string.IsNullOrEmpty(AcceptButtonText) && !IsProcessing;

		public bool IsProcessing
		{
			get => _isProcessing;
			set
			{
				_isProcessing = value;
				OnPropertyChanged(nameof(IsProcessing));
				OnPropertyChanged(nameof(VisibilityAcceptButton));
				OnPropertyChanged(nameof(VisibilityRejectButton));
			}
		}

		/// <summary>
		/// Видимость кнопки на отклонение.
		/// </summary>
		public bool VisibilityRejectButton => (_document?.Actions.HasFlag(Actions.Reject) ?? false) && !IsProcessing;

		public RelayCommand AcceptCommand => new RelayCommand(AcceptDocument);

		public RelayCommand RejectCommand => new RelayCommand(RejectDocument);

		private async void RejectDocument()
		{
			var confirmation = await Application.Current.MainPage.DisplayAlert("Отклонение", "Вы уверены, что хотите отклонить документ?", "Да", "Нет");
			if (confirmation)
			{
				IsProcessing = true;
				await Task.Run(() =>
				{
					Thread.Sleep(3000);
					IsProcessing = false;
					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Готово", "Документ отклонён", "Ок");
						OnDeleteDoc();
						Application.Current.MainPage.Navigation.PopAsync();
					});
				});
			}
		}

		private void AcceptDocument()
		{
			IsProcessing = true;
			Task.Run(() =>
			{
				Thread.Sleep(3000);
				var result = string.Empty;
				switch (_actionForDocument)
				{
					case Actions.AcceptWithTicket:
					case Actions.Accept:
						result = "Принят";
						break;
					case Actions.Sign:
						result = "Подписан";
						break;
					case Actions.Send:
						result = "Отправлен";
						break;
				}
				IsProcessing = false;
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Готово", $"Документ {result}", "Ок");
					OnDeleteDoc();
					Application.Current.MainPage.Navigation.PopAsync();
				});
			});
		}

		public event EventHandler DeleteDoc;

		protected void OnDeleteDoc()
		{
			DeleteDoc?.Invoke(_document, EventArgs.Empty);
		}
	}
}