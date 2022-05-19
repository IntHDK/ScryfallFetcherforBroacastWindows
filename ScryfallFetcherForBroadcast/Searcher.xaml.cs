using ScryfallFetch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ScryfallFetcherForBroadcast
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Searcher : Window
    {
        private CardViewWindow cardViewWindow;
        private ScryfallFetcher scryfallFetcher;


        private async Task Search()
        {
            SearchStatus.Content = "검색중...";
            SearchStatus.Foreground = new SolidColorBrush(Colors.DarkGray);
            var obj = await scryfallFetcher.FetchNamed(Keyword.Text);
            if(obj != null)
            {
                SearchStatus.Content = "검색 성공";
                SearchStatus.Foreground = new SolidColorBrush(Colors.ForestGreen);
                if (obj.Image_uris != null)
                {
                    string Imageuri = "";
                    if(obj.Image_uris.Border_crop != string.Empty)
                    {
                        Imageuri = obj.Image_uris.Border_crop;
                    }
                    else if(obj.Image_uris.Png != string.Empty)
                    {
                        Imageuri = obj.Image_uris.Png;
                    }
                    else if (obj.Image_uris.Large != string.Empty){
                        Imageuri = obj.Image_uris.Large;
                    }
                    else if (obj.Image_uris.Normal != string.Empty)
                    {
                        Imageuri = obj.Image_uris.Normal;
                    }
                    else if (obj.Image_uris.Small != string.Empty)
                    {
                        Imageuri = obj.Image_uris.Small;
                    }
                    if(Imageuri != string.Empty)
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(Imageuri);
                        bitmap.EndInit();
                        cardViewWindow.SetFaceSingle(bitmap);
                    }
                }
                else if (obj.Card_faces != null)
                {
                    List<BitmapImage> bitmaps = new List<BitmapImage>();
                    foreach (var face in obj.Card_faces)
                    {
                        string Imageuri = "";
                        if(face != null)
                        {
                            if(face.Image_uris != null)
                            {
                                if (face.Image_uris.Border_crop != string.Empty)
                                {
                                    Imageuri = face.Image_uris.Border_crop;
                                }
                                else if (face.Image_uris.Png != string.Empty)
                                {
                                    Imageuri = face.Image_uris.Png;
                                }
                                else if (face.Image_uris.Large != string.Empty)
                                {
                                    Imageuri = face.Image_uris.Large;
                                }
                                else if (face.Image_uris.Normal != string.Empty)
                                {
                                    Imageuri = face.Image_uris.Normal;
                                }
                                else if (face.Image_uris.Small != string.Empty)
                                {
                                    Imageuri = face.Image_uris.Small;
                                }
                                if (Imageuri != string.Empty)
                                {
                                    BitmapImage bitmap = new BitmapImage();
                                    bitmap.BeginInit();
                                    bitmap.UriSource = new Uri(Imageuri);
                                    bitmap.EndInit();
                                    bitmaps.Add(bitmap);
                                }
                            }
                        }
                        
                    }
                    if(bitmaps.Count > 0)
                    {
                        cardViewWindow.SetFace(bitmaps);
                    }
                }
            }
            else
            {
                SearchStatus.Content = "검색 실패";
                SearchStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        public Searcher()
        {
            cardViewWindow = new CardViewWindow();
            scryfallFetcher = new ScryfallFetcher();
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            cardViewWindow?.Show();
            Search();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ViewOpenButton_Click(object sender, RoutedEventArgs e)
        {
            cardViewWindow?.Show();
        }

        private void Keyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

    }
}
