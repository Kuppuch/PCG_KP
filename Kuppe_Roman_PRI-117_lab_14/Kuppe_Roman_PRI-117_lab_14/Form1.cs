using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.Platform.Windows;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.DevIl;
using System.Windows.Input;

namespace Kuppe_Roman_PRI_117_lab_14 {
    public partial class Form1: Form {
        public Form1() {
            InitializeComponent();
            AnT.InitializeContexts();
            AnT.MouseWheel += AnT_MouseWheel;
        }

        private void AnT_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e) {
            c = c + e.Delta / 100f;
        }

        // вспомогательные переменные - в них будут хранится обработанные значения,
        // полученные при перетаскивании ползунков пользователем
        double a = 0, b = 0, c = -5, dX = 0, dY = 0, dZ = 0, zoom = 1; // выбранные оси
        int os_x = 1, os_y = 0, os_z = 0;

        // режим сеточной визуализации
        bool Wire = false;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            // в зависимости от выбранного режима
            switch (comboBox1.SelectedIndex) {

                // устанавливаем необходимую ось (будет испльзовано в функции glRotate**)
                case 0: {

                    os_x = 1;
                    os_y = 0;
                    os_z = 0;
                    break;

                }
                case 1: {

                    os_x = 0;
                    os_y = 1;
                    os_z = 0;
                    break;

                }
                case 2: {

                    os_x = 0;
                    os_y = 0;
                    os_z = 1;
                    break;

                }

            }
        }

        private void AnT_Load(object sender, EventArgs e) {
            
        }

        private void Form1_Load(object sender, EventArgs e) {
            // инициализация бибилиотеки glut
            Glut.glutInit();
            // инициализация режима экрана
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            // установка цвета очистки экрана (RGBA)
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // активация проекционной матрицы
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очистка матрицы
            Gl.glLoadIdentity();

            // установка перспективы
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 200);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            // начальная настройка параметров openGL (тест глубины, освещение и первый источник света)
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);

            // установка первых элементов в списках combobox
            comboBox1.SelectedIndex = 0;
            //comboBox2.SelectedIndex = 0;

            // активация таймера, вызывающего функцию для визуализации
            RenderTimer.Start();
            // опции для загрузки файла
            openFileDialog1.Filter = "ase files (*.ase)|*.ase|All files (*.*)|*.*";
        }

        private void button1_Click(object sender, EventArgs e) {

        }

        private void RenderTimer_Tick(object sender, EventArgs e) {
            //bool v = Keyboard.IsKeyDown(Key.Up);
            if (Keyboard.IsKeyDown(Key.W)) {
                dX = dX + 2;
                os_x = 1;
                os_y = 0;
                os_z = 0;
            }
            if (Keyboard.IsKeyDown(Key.S)) {
                dX = dX - 2;
                os_x = 1;
                os_y = 0;
                os_z = 0;
            }
            if (Keyboard.IsKeyDown(Key.A)) {
                dY = dY + 2;
                os_x = 0;
                os_y = 1;
                os_z = 0;
            }
            if (Keyboard.IsKeyDown(Key.D)) {
                dY = dY - 2;
                os_x = 0;
                os_y = 1;
                os_z = 0;
            }
            if (Keyboard.IsKeyDown(Key.Q)) {
                dZ = dZ + 2;
                os_x = 0;
                os_y = 0;
                os_z = 1;
            }
            if (Keyboard.IsKeyDown(Key.E)) {
                dZ = dZ - 2;
                os_x = 0;
                os_y = 0;
                os_z = 1;
            }
            if (Keyboard.IsKeyDown(Key.Z)) {
                c = c - 0.5;
            }
            if (Keyboard.IsKeyDown(Key.X)) {
                c = c + 0.5;
            }
            // вызываем функцию отрисовки сцены
            Draw();
        }

        private void загрузитьМодельToolStripMenuItem_Click(object sender, EventArgs e) {

        }
        anModelLoader Model = null;
        private void выбратьМодельДляЗагрузкиToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                Model = new anModelLoader();
                Model.LoadModel(openFileDialog1.FileName);
                RenderTimer.Start();
            }
        }

        // функция отрисовки
        private void Draw() {

            // очистка буфера цвета и буфера глубины
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glClearColor(255, 255, 255, 1);
            // очищение текущей матрицы
            Gl.glLoadIdentity();

            // помещаем состояние матрицы в стек матриц, дальнейшие трансформации затронут только визуализацию объекта
            Gl.glPushMatrix();
            // производим перемещение в зависимости от значений, полученных при перемещении ползунков
            Gl.glTranslated(a, b, c);
            // поворот по установленной оси
            Gl.glRotated(dY, 0, 1, 0);
            Gl.glRotated(dX, 1, 0, 0);
            Gl.glRotated(dZ, 0, 0, 1);
            // и масштабирование объекта
            Gl.glScaled(zoom, zoom, zoom);

            if (Model != null)
                Model.DrawModel();

           /* // в зависимсоти от установленного типа объекта
            switch (comboBox2.SelectedIndex) {

                // рисуем нужный объект, используя фунции бибилиотеки GLUT
                case 0: {

                    if (Wire) // если установлен сеточный режим визуализации
                        Glut.glutWireSphere(2, 16, 16); // сеточная сфера
                    else
                        Glut.glutSolidSphere(2, 16, 16); // полигональная сфера
                    break;

                }
                case 1: {

                    if (Wire) // если установлен сеточный режим визуализации
                        Glut.glutWireCylinder(1, 2, 32, 32); // цилиндр
                    else
                        Glut.glutSolidCylinder(1, 2, 32, 32);
                    break;

                }
                case 2: {

                    if (Wire) // если установлен сеточный режим визуализации
                        Glut.glutWireCube(2); // куб
                    else
                        Glut.glutSolidCube(2);
                    break;

                }
                case 3: {

                    if (Wire) // если установлен сеточный режим визуализации
                        Glut.glutWireCone(2, 3, 32, 32); // конус
                    else
                        Glut.glutSolidCone(2, 3, 32, 32);
                    break;

                }
                case 4: {

                    if (Wire) // если установлен сеточный режим визуализации
                        Glut.glutWireTorus(0.2, 2.2, 32, 32); // тор
                    else
                        Glut.glutSolidTorus(0.2, 2.2, 32, 32);
                    break;

                }

            }*/

            // возвращаем состояние матрицы
            Gl.glPopMatrix();

            // завершаем рисование
            Gl.glFlush();

            // обновлем элемент AnT
            AnT.Invalidate();

        }

        private void button2_Click(object sender, EventArgs e) {
            Repeater(button2, 1, dX, -1);
        }

        public void Repeater(Button btn, int interval, double axis, int index) {
            var timer = new Timer { Interval = interval };
            //timer.Tick += (sender, e) => DoProgress();
            timer.Tick += (sender, e) => { axis += index; };
            btn.MouseDown += (sender, e) => timer.Start();
            btn.MouseUp += (sender, e) => timer.Stop();
            btn.Disposed += (sender, e) =>
            {
                timer.Stop();
                timer.Dispose();
            };
        }

        private void button3_Click(object sender, EventArgs e) {
            Repeater(button3, 1, dX, 1);
        }

        private void AnT_Scroll(object sender, ScrollEventArgs e) {
            Console.WriteLine(e);
        }

        private void AnT_Click(object sender, EventArgs e) {

        }

        private void trackBar4_Scroll_1(object sender, EventArgs e) {
            // переводим значение, установившееся в элементе trackBar, в необходимый нам формат
            dX = (double)trackBar4.Value;
            // подписываем это значение в label элементе под данным ползунком
            label10.Text = dX.ToString();
        }

        private void trackBar5_Scroll_1(object sender, EventArgs e) {
            // переводим значение, установившееся в элементе trackBar, в необходимый нам формат
            zoom = (double)trackBar5.Value / 1000.0;
            // подписываем это значение в label элементе под данным ползунком
            label11.Text = zoom.ToString();
        }

        private void trackBar3_Scroll_1(object sender, EventArgs e) {
            // переводим значение, установившееся в элементе trackBar, в необходимый нам формат
            c = (double)trackBar3.Value / 1000.0;
            // подписываем это значение в label элементе под данным ползунком
            label7.Text = c.ToString();
        }

        private void trackBar2_Scroll_1(object sender, EventArgs e) {
            // переводим значение, установившееся в элементе trackBar, в необходимый нам формат
            b = (double)trackBar2.Value / 1000.0;
            // подписываем это значение в label элементе под данным ползунком
            label5.Text = b.ToString();
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e) {
            // переводим значение, установившееся в элементе trackBar, в необходимый нам формат
            a = (double)trackBar1.Value / 1000.0;
            // подписываем это значение в label элементе под данным ползунком
            label3.Text = a.ToString();
        }

        // изменения значения checkBox
        private void checkBox1_CheckedChanged(object sender, EventArgs e) {

            // если отмечен
            if (checkBox1.Checked) {

                // устанавливаем сеточный режим визуализации
                Wire = true;

            } else {

                // иначе - полигональная визуализация
                Wire = false;

            }

        }

    }
}
