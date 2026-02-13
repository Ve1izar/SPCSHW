using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ДЗ_Files
{
    // Модель для відображення елемента
    public class FileSystemItem
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string Type { get; set; }
        public string DateModified { get; set; }
        public string Size { get; set; }
    }

    public partial class MainWindow : Window
    {
        private string _currentPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OpenDirectory(@"C:\");
        }
        private void OpenDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                MessageBox.Show("Шлях не знайдено!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var items = new List<FileSystemItem>();
                var dirInfo = new DirectoryInfo(path);

                foreach (var dir in dirInfo.GetDirectories())
                {
                    items.Add(new FileSystemItem
                    {
                        Icon = "📁",
                        Name = dir.Name,
                        FullPath = dir.FullName,
                        Type = "Dir",
                        DateModified = dir.LastWriteTime.ToString("g"),
                        Size = "<DIR>"
                    });
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    items.Add(new FileSystemItem
                    {
                        Icon = "📄",
                        Name = file.Name,
                        FullPath = file.FullName,
                        Type = "File",
                        DateModified = file.LastWriteTime.ToString("g"),
                        Size = (file.Length / 1024) + " KB"
                    });
                }

                FilesListView.ItemsSource = items;
                _currentPath = path;
                PathTextBox.Text = path;
                StatusText.Text = $"Елементів: {items.Count}";
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Немає доступу до цієї папки.", "Відмова", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parent = Directory.GetParent(_currentPath);
                if (parent != null)
                {
                    OpenDirectory(parent.FullName);
                }
            }
            catch { }
        }

        private void FilesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenSelectedItem();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectedItem();
        }

        private void OpenSelectedItem()
        {
            if (FilesListView.SelectedItem is FileSystemItem selectedItem)
            {
                if (selectedItem.Type == "Dir")
                {
                    OpenDirectory(selectedItem.FullPath);
                }
                else
                {
                    ShowFileContent(selectedItem.FullPath);
                }
            }
        }

        private void ShowFileContent(string filePath)
        {
            try
            {
                string ext = System.IO.Path.GetExtension(filePath).ToLower();

                if (ext == ".txt" || ext == ".cs" || ext == ".xml" || ext == ".json" || ext == ".log" || ext == ".ini")
                {
                    string content = File.ReadAllText(filePath);

                    Window viewWindow = new Window
                    {
                        Title = $"Перегляд: {System.IO.Path.GetFileName(filePath)}",
                        Width = 600,
                        Height = 400
                    };

                    TextBox textBox = new TextBox
                    {
                        Text = content,
                        IsReadOnly = true,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                        FontFamily = new System.Windows.Media.FontFamily("Consolas")
                    };

                    viewWindow.Content = textBox;
                    viewWindow.Show();
                }
                else
                {
                    var result = MessageBox.Show("Цей файл може бути бінарним або занадто великим.\nВідкрити його у стандартній програмі?",
                        "Відкриття файлу", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося відкрити файл: {ex.Message}");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (FilesListView.SelectedItem is FileSystemItem selectedItem)
            {
                var result = MessageBox.Show($"Ви впевнені, що хочете видалити '{selectedItem.Name}'?",
                    "Підтвердження видалення", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (selectedItem.Type == "Dir")
                        {
                            Directory.Delete(selectedItem.FullPath, true);
                        }
                        else
                        {
                            File.Delete(selectedItem.FullPath);
                        }
                        OpenDirectory(_currentPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при видаленні: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDirectory(PathTextBox.Text);
        }

        private void PathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OpenDirectory(PathTextBox.Text);
            }
        }
    }
}