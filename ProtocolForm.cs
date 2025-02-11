namespace Lab1_ASPPR
{
    public partial class ProtocolForm : Form
    {
        public ProtocolForm(string protocol)
        {
            InitializeComponent();
            textBoxProtocol.Text = protocol;
            textBoxProtocol.ScrollBars = ScrollBars.Vertical;
        }
    }
}
