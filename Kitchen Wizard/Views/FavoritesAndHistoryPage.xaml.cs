namespace Kitchen_Wizard.Views;
//[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class FavoritesAndHistoryPage : ContentPage
{
    //string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
    //public string ItemId
    //{
    //    set { LoadNote(value); }
    //}
    public FavoritesAndHistoryPage()
	{
		InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";


        //if (File.Exists(_fileName))
        //    TextEditor.Text = File.ReadAllText(_fileName);
    }

}