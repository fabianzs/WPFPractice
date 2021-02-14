using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RoutedEventsSpyGlass
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DateTime before;
        private readonly FontFamily fontfam = new FontFamily("arial");

        public MainWindow()
        {
            InitializeComponent();

            //These are the elements that we will be focusing on
            UIElement[] elements = {grid, btn, text};

            //Attach event handler to the events
            foreach (var element in elements)
            {
                element.PreviewKeyDown += DoEverythingEventHandler;
                element.PreviewKeyUp += DoEverythingEventHandler;
                element.PreviewTextInput += DoEverythingEventHandler;
                element.KeyDown += DoEverythingEventHandler;
                element.KeyUp += DoEverythingEventHandler;
                element.TextInput += DoEverythingEventHandler;

                element.MouseDown += DoEverythingEventHandler;
                element.MouseUp += DoEverythingEventHandler;
                element.PreviewMouseUp += DoEverythingEventHandler;
                element.PreviewMouseDown += DoEverythingEventHandler;

                element.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(DoEverythingEventHandler));
            }
        }

        private void DoEverythingEventHandler(object sender, RoutedEventArgs args)
        {
            var sp = new StackPanel();

            //Insert a separator if 100ms has lapsed.
            //This will increase readability
            var now = DateTime.Now;
            if (now - before > TimeSpan.FromMilliseconds(100))
            {
                var sp_blank = new StackPanel
                {
                    Height = 20,
                    Background = Brushes.Gray
                };
                sp.Children.Add(sp_blank);
            }


            before = now;

            var width = 60;
            //Specify the orientation of the stackpanel.
            sp.Orientation = Orientation.Horizontal;


            //Add a new textblock, format and populate it and add it to the stackpanel
            var tb1 = new TextBlock();
            FormatTextBox(args, tb1, width * 2, args.RoutedEvent.Name);
            sp.Children.Add(tb1);

            //Add a new textblock, format and populate it and add it to the stackpanel
            var tb2 = new TextBlock();
            FormatTextBox(args, tb2, width, ShrinkTheName(sender));
            sp.Children.Add(tb2);

            //Add a new textblock, format and populate it and add it to the stackpanel
            var tb3 = new TextBlock();
            FormatTextBox(args, tb3, width, ShrinkTheName(args.Source));
            sp.Children.Add(tb3);

            //Add a new textblock, format and populate it and add it to the stackpanel
            var tb4 = new TextBlock();
            FormatTextBox(args, tb4, width, ShrinkTheName(args.OriginalSource));
            sp.Children.Add(tb4);

            //Add a new textblock, format and populate it and add it to the stackpanel
            var tb5 = new TextBlock();
            FormatTextBox(args, tb5, width, args.RoutedEvent.RoutingStrategy.ToString());
            sp.Children.Add(tb5);

            //Finally add the stackpanel that we just created to the scrollviewer stackpanel and scroll to the bottom
            myStackPanel.Children.Add(sp);
            ScrollViewer.ScrollToBottom();
        }

        private void FormatTextBox(RoutedEventArgs args, TextBlock tb, int width, string content)
        {
            tb.FontFamily = fontfam;
            tb.Width = width;
            tb.Foreground = args.RoutedEvent.RoutingStrategy == RoutingStrategy.Bubble ? Brushes.Green : Brushes.Red;
            tb.Margin = new Thickness(40, 5, 40, 5);
            tb.Text = $"{content}";
        }

        private string ShrinkTheName(object obj)
        {
            //Remove the namespace and class information for readability
            var str = obj.GetType().ToString().Split('.');
            return str[str.Length - 1];
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //Clear the view
            myStackPanel.Children.Clear();
        }
    }
}