using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

using WinForms = System.Windows.Forms;

namespace Theme_Project
{
    public class FileSearchResult
    {
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public int Count { get; set; }
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<FileSearchResult> SearchResults { get; set; }
        private CancellationTokenSource? _cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
            SearchResults = new ObservableCollection<FileSearchResult>();
            ResultsListView.ItemsSource = SearchResults;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new WinForms.FolderBrowserDialog())
            {
                WinForms.DialogResult result = dialog.ShowDialog();
                if (result == WinForms.DialogResult.OK)
                {
                    DirectoryTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchButton.Content.ToString() == "Стоп")
            {
                _cancellationTokenSource?.Cancel();
                return;
            }

            string directoryPath = DirectoryTextBox.Text;
            string searchWord = SearchWordTextBox.Text;

            if (string.IsNullOrWhiteSpace(directoryPath) || !Directory.Exists(directoryPath))
            {
                System.Windows.MessageBox.Show("Вкажіть коректний шлях до директорії.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(searchWord))
            {
                System.Windows.MessageBox.Show("Введіть слово для пошуку.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SearchResults.Clear();
            SearchButton.Content = "Стоп";
            StatusTextBlock.Text = "Аналіз файлів...";
            SearchProgressBar.Value = 0;
            SummaryTextBlock.Text = "";

            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            try
            {
                await RunSearchAsync(directoryPath, searchWord, token);
            }
            catch (OperationCanceledException)
            {
                StatusTextBlock.Text = "Пошук скасовано користувачем.";
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Виникла помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                SearchButton.Content = "Пошук";
                _cancellationTokenSource = null;
            }
        }

        private async Task RunSearchAsync(string path, string word, CancellationToken token)
        {
            var allFiles = await Task.Run(() =>
            {
                try
                {
                    return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                }
                catch (UnauthorizedAccessException)
                {
                    return new string[0];
                }
            });

            if (allFiles.Length == 0)
            {
                StatusTextBlock.Text = "Файлів не знайдено (або немає доступу).";
                return;
            }

            SearchProgressBar.Maximum = allFiles.Length;
            int processedCount = 0;
            int foundFilesCount = 0;

            var progress = new Progress<int>(percent =>
            {
                SearchProgressBar.Value = percent;
                StatusTextBlock.Text = $"Оброблено {percent} з {allFiles.Length} файлів...";
            });

            await Task.Run(() =>
            {
                foreach (var file in allFiles)
                {
                    if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

                    try
                    {
                        string content = File.ReadAllText(file);
                        int count = Regex.Matches(content, Regex.Escape(word), RegexOptions.IgnoreCase).Count;

                        if (count > 0)
                        {
                            foundFilesCount++;

                            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                            {
                                SearchResults.Add(new FileSearchResult
                                {
                                    FileName = Path.GetFileName(file),
                                    FilePath = file,
                                    Count = count
                                });
                            });
                        }
                    }
                    catch { }

                    Interlocked.Increment(ref processedCount);
                    if (processedCount % 10 == 0 || processedCount == allFiles.Length)
                    {
                        ((IProgress<int>)progress).Report(processedCount);
                    }
                }
            });

            StatusTextBlock.Text = "Пошук завершено.";
            SummaryTextBlock.Text = $"Всього знайдено файлів зі словом: {foundFilesCount}";
        }
    }
}