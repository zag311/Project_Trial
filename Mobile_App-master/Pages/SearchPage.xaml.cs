using Recraft.Models;
using Recraft.Services;

namespace Recraft.Pages
{
    public partial class SearchPage : ContentPage
    {
        private readonly DatabaseService _dbService = new();

        public SearchPage()
        {
            InitializeComponent();
        }

        private async void OnSearchButtonPressed(object sender, EventArgs e)
        {
            var keyword = IdeaSearchBar.Text;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var results = await _dbService.SearchIdeasAsync(keyword);
                SearchResultsView.ItemsSource = results;
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Idea selectedIdea)
            {
                TitleLabel.Text = selectedIdea.Title;
                DescriptionLabel.Text = selectedIdea.Description;
                ItemTypeLabel.Text = $"Type: {selectedIdea.ItemType}";
                IdeaImage.Source = ImageSource.FromFile(selectedIdea.ImagePath);
                SearchResultsView.SelectedItem = null;
                DetailContainer.IsVisible = true;
            }
        }
    }
}