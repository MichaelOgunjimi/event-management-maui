using EventyMaui.Views;

namespace EventyMaui.Views;

public partial class StartPage : ContentPage
{
    private bool isInitialStart = true;

    public StartPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (isInitialStart)
        {
            // This is the initial startup, play the animation
            PlayAnimation();
            isInitialStart = false;
        }
        else
        {
            // The page is being navigated to from another page, don't play the animation
            // You can reset the properties here if needed
            FirstPageLabel.TranslationY = 0;
            FirstPageLabel.FontSize = 42;
            HomePageImage.Opacity = 1;
            BottomGrid.Opacity = 1;
        }
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        isInitialStart = false;
    }

    private void PlayAnimation()
    {

        // Initial position for firstLabel in the center
        FirstPageLabel.TranslationY = 300; // Start at the center
        FirstPageLabel.FontSize = 72;
        HomePageImage.Opacity = 0;
        BottomGrid.Opacity = 0;


        var labelMoveBackAnimation = new Animation(v =>
        {
            FirstPageLabel.TranslationY = v; // Move back to its designated position
        }, 300, 0, Easing.CubicInOut);
        var labelChangeSizeAnimation = new Animation(v =>
        {
            FirstPageLabel.FontSize = v;
        }, 72, 42, Easing.CubicInOut);

        var imageFadeInAnimation = new Animation(v => HomePageImage.Opacity = v, 0, 1, Easing.CubicIn);
        var bottomGridFadeInAnimation = new Animation(v => BottomGrid.Opacity = v, 0, 1, Easing.CubicIn);

        var labelAnimationSequence = new Animation
        {
            { 0.5, 1, labelMoveBackAnimation },
            { 0.6, 1, labelChangeSizeAnimation }
        };

        var parentAnimation = new Animation
        {
            { 0, 1, labelAnimationSequence },
            { 0.8, 1, imageFadeInAnimation },
            { 0.9, 1, bottomGridFadeInAnimation }
        };

        parentAnimation.Commit(this, "PageAnimations", 16, 3000, finished: (v, c) => { });
    }

    private async void ExploreNow_Clicked(System.Object sender, System.EventArgs e)
    {
        // Navigate to the HomePage
        await Shell.Current.GoToAsync(nameof(HomePage));
    }

    //public async void CaptureImage_Clicked(System.Object sender, System.EventArgs e)
    //{
    //    // Navigate to the HomePage
    //    //await Shell.Current.GoToAsync(nameof(HomePage));


    //    if (!MediaPicker.Default.IsCaptureSupported)
    //    {
    //        await DisplayAlert("Not supported", "Capture is not supported on this device", "OK");
    //        return;
    //    } else
    //    {
    //        FileResult file = await MediaPicker.Default.CapturePhotoAsync();
    //        if (file == null)
    //        {
    //            string message = "Unable to capture photo";
    //            await DisplayAlert("Error", message, "OK");
    //            return;
    //        } else
    //        {

    //            string localfilePath = Path.Combine(FileSystem.CacheDirectory, file.FileName);
    //            using Stream sourceStream = await file.OpenReadAsync();
    //            using FileStream targetStream = File.OpenWrite(localfilePath);
    //            await sourceStream.CopyToAsync(targetStream);
    //            string message = "Photo captured" + localfilePath;
    //            await DisplayAlert("Success", message, "OK");
    //            return;
    //        }
    //    }

    //}

}