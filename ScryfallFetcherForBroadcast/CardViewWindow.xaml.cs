using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ScryfallFetcherForBroadcast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CardViewWindow : Window
    {
        public int CurrentFace { get; set; }
        public List<BitmapImage> CardFaceBitmaps { get; set; }

        public CardViewWindow()
        {
            CardFaceBitmaps = new List<BitmapImage>();
            CurrentFace = 0;

            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void SetFace(List<BitmapImage> images)
        {
            if(images.Count > 0)
            {
                CardFaceBitmaps.Clear();
                CardFaceBitmaps = images;
                CurrentFace = 0;
                CardImgView.Source = CardFaceBitmaps[CurrentFace];
            }
        }
        public void SetFaceSingle(BitmapImage image)
        {
            CardFaceBitmaps.Clear();
            CardFaceBitmaps.Add(image);
            CurrentFace = 0;
            CardImgView.Source = CardFaceBitmaps[CurrentFace];
        }

        private void CardImgView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(CardFaceBitmaps.Count > 0)
            {
                CurrentFace++;
                if (CurrentFace >= CardFaceBitmaps.Count)
                {
                    CurrentFace = 0;
                }
                CardImgView.Source = CardFaceBitmaps[CurrentFace];
            }
            
        }
    }
}
