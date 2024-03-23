using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using WpfTutorialSamples.Dialogs;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Interaction logic for UserListControl.xaml
    /// </summary>
    public partial class FilmListControl : UserControl
    {
        private DatabasePeleMele _db;
        private List<Film> _films;

        public FilmListControl()
        {
            InitializeComponent();
            GenerateFilmList();
        }

        private void GetFilms()
        {
            _db = new DatabasePeleMele();
            _films = _db.ReadFilms();
        }

        private void GenerateFilmList()
        {
            GetFilms();
            foreach (Film film in _films)
            {
                ListBoxItem itemFilm = new ListBoxItem();
                itemFilm.Content = film;
                lstFilms.Items.Add(itemFilm);
            }
        }

        private void btn_ajoutFilm_Click(object sender, RoutedEventArgs e)
        {
            PopUpAjoutFilm inputDialog = new PopUpAjoutFilm();
            if (inputDialog.ShowDialog() == true)
            {
                string result = inputDialog.Answer;
                Film film = new Film();
                film.Titre = result;
                film.Projections = new List<List<string>>();
                _db.CreateFilm(film);
                lstFilms.Items.Clear();
                GenerateFilmList();
            }
        }

        private void LstFilms_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFilms.SelectedItem != null)
            {
                var selectedItem = (ListBoxItem)lstFilms.SelectedItem;

                ProgramProjectionFilm programProjectionFilm =
                    new ProgramProjectionFilm(selectedItem.Content.ToString());
                if (programProjectionFilm.ShowDialog() == true)
                {
                    List<string> result = programProjectionFilm.Answer;
                    Film film = (Film)selectedItem.Content;
                    film.Projections.Add(result);
                    _db.UpdateFilm(film);
                    lstFilms.Items.Clear();
                    GenerateFilmList();
                }
            }
        }
    }
}