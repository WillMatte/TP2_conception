using System.Windows;
using DnsClient.Protocol;

namespace CineQuebec.Windows.View;

public partial class ProgramProjectionFilm : Window
{
    List<string> _projections = new List<string>();

    public ProgramProjectionFilm(string titreFilm)
    {
        InitializeComponent();
        lblTitrePage.Content = titreFilm;
    }

    private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
    {
        this._projections.Add(txtDate.Text);
        this._projections.Add(txtHeure.Text);
        this.DialogResult = true;
    }

    public List<string> Answer
    {
        get { return _projections; }
    }
}