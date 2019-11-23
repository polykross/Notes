using System.Windows;

namespace Notes.Tools.Controls
{
    /// <summary>
    /// Interaction logic for LabelAndTextBoxControl.xaml
    /// </summary>
    public partial class LabelAndTextBoxControl
    {
        public LabelAndTextBoxControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register
        (
            "Text",
            typeof(string),
            typeof(LabelAndTextBoxControl),
            new PropertyMetadata(null)
        );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(CaptionProperty, value);
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register
        (
            "Caption",
            typeof(string),
            typeof(LabelAndTextBoxControl),
            new PropertyMetadata(null)
        );

        public string Caption
        {
            get => (string)GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }

        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register
        (
            "MaxLength",
            typeof(int),
            typeof(LabelAndTextBoxControl),
            new PropertyMetadata(10)
        );
        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }
    }
}
