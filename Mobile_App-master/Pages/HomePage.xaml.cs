using Microsoft.Maui.Platform;
using Recraft.Models;
using Recraft.Services;

namespace Recraft.Pages
{
    public partial class HomePage : ContentPage
    {
        private readonly DatabaseService _dbService = new();
        private List<Idea> _ideas = new();
        private int _currentIndex = 0;

        public HomePage()
        {
            InitializeComponent();
            LoadIdeas();

            // Subscribe to MessagingCenter in case you use MessagingCenter from SubmitIdeaPage
            MessagingCenter.Subscribe<SubmitIdeaPage>(this, "IdeaSubmitted", async (sender) =>
            {
                await LoadIdeas();
            });
        }

        private async Task LoadIdeas()
        {
            _ideas = await _dbService.GetIdeasAsync();
            if (_ideas.Count > 0)
            {
                _currentIndex = 0;
                ShowIdea(_ideas[_currentIndex]);
                // Ensure scroll to top on initial load as well
                await MainScrollView.ScrollToAsync(0, 0, true);
            }
        }

        private void ShowIdea(Idea idea)
        {   
            TitleLabel.Text = idea.Title;
            DescriptionLabel.Text = idea.Description;
            ItemTypeLabel.Text = $"Type: {idea.ItemType}";
            IdeaImage.Source = ImageSource.FromFile(idea.ImagePath);

            BackButton.IsEnabled = _currentIndex > 0;
        }

        private async void OnNextClicked(object sender, EventArgs e) // Changed to async
        {
            if (_ideas.Count == 0) return;

            _currentIndex = (_currentIndex + 1) % _ideas.Count;
            ShowIdea(_ideas[_currentIndex]);
            await MainScrollView.ScrollToAsync(0, 0, true); // Scroll to top after changing idea
        }

        private async void OnBackClicked(object sender, EventArgs e) // Changed to async
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                ShowIdea(_ideas[_currentIndex]);
                await MainScrollView.ScrollToAsync(0, 0, true); // Scroll to top after changing idea
            }
        }
    }
}