using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transport;

namespace WindowsFormsApp1
{
    public partial class Registration : Form
    {       
      //проверка логина
        private bool IsLogin
        {
            get
            {
                bool been = false;
                string loginUser = textBoxLogin.Text;
                string passwordUser = textBoxPassword.Text;

                DatabaseManager _databaseManager = new DatabaseManager();
                DataTable _dataTable = new DataTable();
                MySqlDataAdapter _mySqlDataAdapter = new MySqlDataAdapter();
                MySqlCommand _mySqlCommand = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @UserLogin AND `password` = @UserPassword", _databaseManager.GetConnection);//выбираем все записи из таблички user где логин = введеному логину и пароль = введеному паролю

                //меняем заглушки на переменные
                _mySqlCommand.Parameters.Add("@UserLogin", MySqlDbType.VarChar).Value = loginUser;
                _mySqlCommand.Parameters.Add("@UserPassword", MySqlDbType.VarChar).Value = passwordUser;

                _mySqlDataAdapter.SelectCommand = _mySqlCommand;//выбираем команду
                _mySqlDataAdapter.Fill(_dataTable);//заполянем данные в табличку ***

                if (_dataTable.Rows.Count > 0)
                {
                    been = true;
                    MessageBox.Show("Такой логин уже есть!\nПопробуйте другой логин!", "Внимание!");
                }
                else
                    been = false;

                return been;
            }
        }

        //проверка пользователя
        private bool IsUser
        {
            get
            {
                /*
                 * Проверяем все данные, которые вводит пользователь на совпадение,
                 * кроме пароля. Так как возможно пользователь уже есть, но он ввел 
                 * другой пароль.
                 */

                bool been = false;

                string loginUser = textBoxLogin.Text;
                string nameUser = textBoxName.Text;
                string surnameUser = textBoxSurname.Text;

                DatabaseManager _databaseManager = new DatabaseManager();
                DataTable _dataTable = new DataTable();
                MySqlDataAdapter _mySqlDataAdapter = new MySqlDataAdapter();
                MySqlCommand _mySqlCommand = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @UserLogin AND `name` = @UserName AND `surname` = @UserSurname", _databaseManager.GetConnection);//выбираем все записи из таблички user где логин = введеному логину и пароль = введеному паролю

                //меняем заглушки на переменные
                _mySqlCommand.Parameters.Add("@UserLogin", MySqlDbType.VarChar).Value = loginUser;
                _mySqlCommand.Parameters.Add("@UserName", MySqlDbType.VarChar).Value = nameUser;
                _mySqlCommand.Parameters.Add("@UserSurname", MySqlDbType.VarChar).Value = surnameUser;

                _mySqlDataAdapter.SelectCommand = _mySqlCommand;//выбираем команду
                _mySqlDataAdapter.Fill(_dataTable);//заполянем данные в табличку

                //проверяем на совпадение
                if (_dataTable.Rows.Count > 0)
                {
                    been = true;
                    if (MessageBox.Show("Такой пользователь уже есть!\nПерейти на вкладку входа?", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //переходим на форму входа
                        Authorization form = new Authorization();
                        this.Hide();
                        form.Show();
                    }
                }
                else
                    been = false;


                return been;
            }
        }


        public Registration()
        {
            InitializeComponent();

            //добавим подсказки в в текстовые поля
            textBoxName.Text = "Введите имя!";
            textBoxName.ForeColor = Color.Gray;

            textBoxSurname.Text = "Введите фамилию!";
            textBoxSurname.ForeColor = Color.Gray;

            textBoxLogin.Text = "Введите логин!";
            textBoxLogin.ForeColor = Color.Gray;

            textBoxPassword.Text = "Введите пароль!";
            textBoxPassword.ForeColor = Color.Gray;
        }

        private void textBoxName_Enter(object sender, EventArgs e)
        {
            if (textBoxName.Text == "Введите имя!")//если введена подсказка
            {
                textBoxName.Text = "";//очищаем содержимое при вводе информации
                textBoxName.ForeColor = Color.Black;
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            if (textBoxName.Text == "")//если выходим из поля и ничего не вводим
            {
                textBoxName.Text = "Введите имя!";//возращаем подсказку
                textBoxName.ForeColor = Color.Gray;
            }
        }

        private void textBoxSurname_Enter(object sender, EventArgs e)
        {
            if (textBoxSurname.Text == "Введите фамилию!")//если введена подсказка
            {
                textBoxSurname.Text = "";//очищаем содержимое при вводе информации
                textBoxSurname.ForeColor = Color.Black;
            }
        }

        private void textBoxSurname_Leave(object sender, EventArgs e)
        {
            if (textBoxSurname.Text == "")//если выходим из поля и ничего не вводим
            {
                textBoxSurname.Text = "Введите фамилию!";//возращаем подсказку
                textBoxSurname.ForeColor = Color.Gray;
            }
        }

        private void textBoxLogin_Enter(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "Введите логин!")//если введена подсказка
            {
                textBoxLogin.Text = "";//очищаем содержимое при вводе информации
                textBoxLogin.ForeColor = Color.Black;
            }
        }

        private void textBoxLogin_Leave(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "")//если выходим из поля и ничего не вводим
            {
                textBoxLogin.Text = "Введите логин!";//возращаем подсказку
                textBoxLogin.ForeColor = Color.Gray;
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "Введите пароль!")//если введена подсказка
            {
                textBoxPassword.Text = "";//очищаем содержимое при вводе информации
                textBoxPassword.ForeColor = Color.Black;
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")//если выходим из поля и ничего не вводим
            {
                textBoxPassword.Text = "Введите пароль!";//возращаем подсказку
                textBoxPassword.ForeColor = Color.Gray;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Authorization form = new Authorization();
            this.Hide();
            form.Show();
        }

        private void ButtonRegistration_Click(object sender, EventArgs e)
        {      

            if (textBoxName.Text == "Введите имя!" || textBoxLogin.Text == "Введите логин!"
                || textBoxPassword.Text == "Введите пароль!" || textBoxSurname.Text == "Введите фамилию!")
            {
                MessageBox.Show("Не все поля введены!", "Внимание!");
                return;
            }


            //прверка на уникальность всех данных
            if (!IsUser)
            {
                //проверка на уникальность логина
                if (!IsLogin)
                {

                    DatabaseManager _databaseManager = new DatabaseManager();
                    MySqlCommand _mySqlCommand = new MySqlCommand("INSERT INTO `users`(`login`, `password`, `name`, `surname`) " +
                        "VALUES (@login,@password,@name,@surname)", _databaseManager.GetConnection);//формируем запрос

                    try
                    {
                        //меняем заглужки на значения
                        _mySqlCommand.Parameters.Add("@login", MySqlDbType.VarChar).Value = textBoxLogin.Text;
                        _mySqlCommand.Parameters.Add("@password", MySqlDbType.VarChar).Value = textBoxPassword.Text;
                        _mySqlCommand.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBoxName.Text;
                        _mySqlCommand.Parameters.Add("@surname", MySqlDbType.VarChar).Value = textBoxSurname.Text;

                        //выполянем запрос
                        _databaseManager.OpenConnection();//открываем соеденения 

                        if (_mySqlCommand.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Аккаует создан!", "Внимание!");

                            DataForm form = new DataForm();
                            this.Hide();
                            form.Show();

                            //запоним кто вошел
                            User user = new User(textBoxLogin.Text);
                        }
                        else
                            MessageBox.Show("Ошибка создания аккаунта!", "Ошибка!");
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка работы с базой данных!", "Ошибка");
                    }
                    finally
                    {
                        _databaseManager.CloseConnection();//закрываем соеденение
                    }
                }
            }
        }
    }
}
