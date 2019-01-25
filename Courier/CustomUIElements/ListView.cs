using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Courier.CustomUIElements
{
    public class ListView : Xamarin.Forms.ListView
    {
        public static BindableProperty ItemClickCommandProperty = BindableProperty.Create(nameof(ItemClickCommand), typeof(ICommand), typeof(ListView), null);

	    public static BindableProperty ItemAppearingCommandProperty = BindableProperty.Create(nameof(ItemAppearingCommand), typeof(ICommand), typeof(ListView), null);

		public ListView()
        {
            ItemTapped += OnItemTapped;
	        ItemAppearing += OnItemAppearing;

        }

        public ICommand ItemClickCommand
        {
            get => (ICommand)GetValue(ItemClickCommandProperty);
            set => SetValue(ItemClickCommandProperty, value);
        }

	    public ICommand ItemAppearingCommand
		{
		    get => (ICommand)GetValue(ItemAppearingCommandProperty);
		    set => SetValue(ItemAppearingCommandProperty, value);
	    }

		private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                if (e.Item != null && ItemClickCommand != null && ItemClickCommand.CanExecute(e))
                {
                    ItemClickCommand.Execute(e.Item);
                    SelectedItem = null;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

	    private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
	    {
		    try
		    {
			    if (e.Item != null && ItemAppearingCommand != null && ItemAppearingCommand.CanExecute(e))
			    {
				    ItemAppearingCommand.Execute(e.Item);
			    }
		    }
		    catch (Exception exception)
		    {
			    Console.WriteLine(exception);
		    }

	    }
	}
}