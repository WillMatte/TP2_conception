﻿using System;
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
using Autofac;
using Autofac.Core;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Driver;
using WpfTutorialSamples.Dialogs;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Interaction logic for UserListControl.xaml
    /// </summary>
    public partial class FilmListControl : UserControl
    {
        private static ContainerBuilder containerBuilder = new ContainerBuilder();
        private IContainer container = containerBuilder.Build();
        
        private DatabasePeleMele _db;
        private List<Film> _films;
        private int _selectedIndex = -1;
        private bool _isProjectionList = false;

        public FilmListControl(IDatabaseProvider databaseProvider)
        {
            _db = databaseProvider.GetDatabasePeleMele(); 
            InitializeComponent();
            btnDelete.IsEnabled = false;
            btnAddProjection.IsEnabled = false;
            GenerateFilmList();
        }

        private void GetFilms()
        {
            _films = _db.ReadFilms();
        }

        private void ClearInterface()
        {
            lstFilms.Items.Clear();
            lstFilms.SelectedIndex = -1;
            _selectedIndex = lstFilms.SelectedIndex;
            btnDelete.IsEnabled = false;
            btnAddProjection.IsEnabled = false;
        }

        private void GenerateFilmList()
        {
            ClearInterface();
            GetFilms();
            btn_changerListe.Content = "Afficher les projections";
            foreach (Film film in _films)
            {
                ListBoxItem itemFilm = new ListBoxItem();
                itemFilm.Content = film;
                lstFilms.Items.Add(itemFilm);
            }
        }

        private void GenerateProjectionList()
        {
            lstFilms.Items.Clear();
            btn_changerListe.Content = "Afficher les films";

            //Meilleur essaie pour afficher les projections
            foreach (Film film in _films)
            {
                for (int i = 0; i < film.Projections.Count; i++)
                {
                    ListBoxItem itemProjection = new ListBoxItem();
                    string affichage = $"{film.Titre} - {film.Projections[i][0]} à {film.Projections[i][1]}";
                    itemProjection.Content = affichage;
                    lstFilms.Items.Add(affichage);
                }
            }
        }

        private void btn_ajoutFilm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PopUpAjoutFilm inputDialog = new PopUpAjoutFilm();
                if (inputDialog.ShowDialog() == true)
                {
                    string result = inputDialog.Answer;
                    Film film = new Film();
                    film.Titre = result;
                    film.Projections = new List<List<string>>();
                    _db.CreateFilm(film);
                    GenerateFilmList();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void LstFilms_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = lstFilms.SelectedIndex;
            if (_selectedIndex != -1)
            {
                btnDelete.IsEnabled = true;
                btnAddProjection.IsEnabled = true;
            }
        }

        private Film? GetSelectedFilm()
        {
            if (_selectedIndex == -1)
                return null;
            ListBoxItem selectedItem = (ListBoxItem)lstFilms.SelectedItem;
            Film selectedFilm = (Film)selectedItem.Content;
            return selectedFilm;
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Film? film = GetSelectedFilm();
                if (film == null)
                    return;
                _db.DeleteFilmById(film.Id);
                GenerateFilmList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private void BtnAddProjection_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Film? film = GetSelectedFilm();
                if (film == null)
                    return;
                ProgramProjectionFilm programProjectionFilm = new ProgramProjectionFilm(film.Titre);
                if (programProjectionFilm.ShowDialog() == true)
                {
                    List<string> result = programProjectionFilm.Answer;
                    film.Projections.Add(result);
                    _db.UpdateFilm(film);
                    GenerateFilmList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private void Btn_changerListe_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _isProjectionList = !_isProjectionList;
                if (!_isProjectionList)
                    GenerateFilmList();
                else
                    GenerateProjectionList();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}