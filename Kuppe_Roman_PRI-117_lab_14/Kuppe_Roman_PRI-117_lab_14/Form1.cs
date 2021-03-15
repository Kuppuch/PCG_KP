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
using System.Threading;

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
        bool crop = true;

        // режим сеточной визуализации
        bool Wire = false;

        

        private void AnT_Load(object sender, EventArgs e) {
            
        }

        private void Help() {
            Thread.Sleep(200);
            MessageBox.Show("W,S - вверх/вниз \n A,D - вправо/влево \n Q,E - поворот вокруг \n Z,X - приблизить/отдалить " +
                "\n Колёсико мыши вверх/вниз - приблизить/отдалить \n R - установка нулевых значений", "Внимание");
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

            // активация таймера, вызывающего функцию для визуализации
            RenderTimer.Start();
            // опции для загрузки файла
            openFileDialog1.Filter = "ase files (*.ase)|*.ase|All files (*.*)|*.*";
            Thread thread = new Thread(new ThreadStart(Help));
            thread.Start(); // запускаем поток
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e) {
            Help();
        }

        private void button1_Click(object sender, EventArgs e) {

        }

        private void button1_Click_1(object sender, EventArgs e) {
            //Zeroing();
            crop = !crop;
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
            if (Keyboard.IsKeyDown(Key.R)) {
                Zeroing();
            }
            // вызываем функцию отрисовки сцены
            if (crop) {
                Draw();
            } else {
                DrawCrop();
            }
            
        }

        public void Zeroing() {
            c = -5;
            dX = 0;
            dY = 0;
            dZ = 0;
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

        private void DrawCrop() {
            // очистка буфера цвета и буфера глубины
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glClearColor(255, 255, 255, 1);
            // очищение текущей матрицы
            Gl.glLoadIdentity();

            // помещаем состояние матрицы в стек матриц, дальнейшие трансформации затронут только визуализацию объекта
            Gl.glPushMatrix();

            Gl.glTranslated(a, b, c);
            // производим перемещение в зависимости от значений, полученных при перемещении ползунков
            //Gl.glTranslated(a, b, c);
            // поворот по установленной оси
            Gl.glRotated(dY, 0, 1, 0);
            Gl.glRotated(dX, 1, 0, 0);
            Gl.glRotated(dZ, 0, 0, 1);
            // и масштабирование объекта
            Gl.glScaled(zoom, zoom, zoom);
            for (var i = 0; i < 1; i++) {
                
                //Glut.glutSolidSphere(1, 16, 16);
                Glut.glutSolidCube(2);

                // возвращаем состояние матрицы
                Gl.glPopMatrix();

                // завершаем рисование
                Gl.glFlush();

                // обновлем элемент AnT
                AnT.Invalidate();
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

            // возвращаем состояние матрицы
            Gl.glPopMatrix();

            // завершаем рисование
            Gl.glFlush();

            // обновлем элемент AnT
            AnT.Invalidate();
        }

        private void AnT_Scroll(object sender, ScrollEventArgs e) {
            Console.WriteLine(e);
        }

        private void AnT_Click(object sender, EventArgs e) {

        }

    }
}
