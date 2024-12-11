using SutIuliaLab7.Models;
namespace SutIuliaLab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
        InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        Shop selectedShop = (ShopPicker.SelectedItem as Shop);
        slist.ShopID = selectedShop.ID;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var selectedItem = listView.SelectedItem as Product; 
        if (selectedItem != null)
        {
            bool isConfirmed = await DisplayAlert("Confirm Delete",
                                                  $"Are you sure you want to delete {selectedItem.Description}?",
                                                  "Yes",
                                                  "No");
            if (isConfirmed)
            {
                await App.Database.DeleteProductAsync(selectedItem); 
                listView.ItemsSource = await App.Database.GetProductsAsync(); 
            }
        }
        else
        {
            await DisplayAlert("Error", "No item selected to delete.", "OK");
        }
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var items = await App.Database.GetShopsAsync();
        ShopPicker.ItemsSource = (System.Collections.IList)items;
        ShopPicker.ItemDisplayBinding = new Binding("ShopDetails");

        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
}