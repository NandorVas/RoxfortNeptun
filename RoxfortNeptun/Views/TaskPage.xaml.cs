using System;
using System.IO;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel;

namespace RoxfortNeptun.Views;

public partial class TaskPage : ContentPage
{
    public TaskPage()
    {
        InitializeComponent();
    }

    private async void OnShareFileClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Select a file to share",
                FileTypes = FilePickerFileType.Images
            });

            if (result == null)
            {
                // User cancelled
                return;
            }

            FileNameLabel.Text = result.FileName;

            // Use a cached copy for cross-platform reliability
            string cachedPath = Path.Combine(FileSystem.CacheDirectory, result.FileName);

            using (var inStream = await result.OpenReadAsync())
            using (var outStream = File.OpenWrite(cachedPath))
            {
                await inStream.CopyToAsync(outStream);
            }

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Share file",
                File = new ShareFile(cachedPath)
            });
        }
        catch (OperationCanceledException)
        {
            // user cancelled operation
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}