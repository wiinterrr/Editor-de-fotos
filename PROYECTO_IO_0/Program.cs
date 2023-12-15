using System;
using System.Linq;
using System.IO; // para poder operar con archivos
using System.Drawing; // para poder operar con fotos

namespace PROYECTO_IO
{
    /***********************************************************************************************************
     * Nombre de la clase: CPixel
     * Funcionalidad: almacena los valores R, G y B, correspondientes al color de un pixel de una imagen
     * Parametros de entrada:
     *  --> R: int. Valor numérico de rojo de 0 a 255
     *  --> G: int. Valor numérico de verde de 0 a 255
     *  --> B: int. Valor numérico de azul de 0 a 255
     ***********************************************************************************************************/
    class CPixel
    {
        public int R;
        public int G;
        public int B;

        public CPixel(int red, int green, int blue)
        {
            R = red;
            G = green;
            B = blue;
        }
    }

    /***********************************************************************************************************
     * Nombre de la clase: CUsuario
     * Funcionalidad: Almacena el nombre y contraseña que el usuario introduzca en la clase usuario.
     * Parametros de entrada:
     *  --> nombre: string nombre
     *  --> contraseña: string contraseña
     ***********************************************************************************************************/
    public class CUsuario // Creamos la clase en la que almacenamos datos de los usuarios
    {
        public string nombre;
        public string contraseña;
        // public string foto;
    }

    /***********************************************************************************************************
     * Nombre de la clase: CLista
     * Funcionalidad: Es un vector que almacena un vector por usuario con su nombre y contraseña.
     ***********************************************************************************************************/
    public class CLista
    {
        public CUsuario[] usuarios = new CUsuario[3333]; //se pueden albergar hasta 3333 usuarios
    }
    
    internal class Program
    {
        /************************************************************************************************************************
         * Nombre de la función: PNGtoMATRIZ
         * Funcionalidad: abre una imagen en formato .png o .jpg y la convierte a una matriz de clase Pixel
         * Parametros de entrada:
         *  --> fotoelegida: String que contiene el nombre de la imagen a abrir. Debe incluir el .png o .jpg
         *  Devuelve:
         *  --> Matriz de clase Pixel: donde cada Pixel contiene los valores R, G y B de cada pixel de la imagen original. 
         *         La forma de la matriz es [anchura, altura]. La posición [0,0] representa el valor de arriba a la izquierda y 
         *         la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
         *  --> Valor null si la imagen no se encuentra.
         * **********************************************************************************************************************/
        static CPixel[,] PNGtoMATRIZ(string fotoelegida) //De .png o .jpg a matriz inimage
        {
            if (fotoelegida != null)
            {
                string inputimagename = fotoelegida;
                Bitmap inimage = new Bitmap(inputimagename); // Abrimos la imagen como Bitmap
                CPixel[,] outimage = new CPixel[inimage.Width, inimage.Height]; //Matriz de Pixeles que devolveremos

                for (int i = 0; i < inimage.Width; i++) //Rellenamos el vector con los valores de la imagen
                {
                    for (int j = 0; j < inimage.Height; j++)
                    {
                        Color color = inimage.GetPixel(i, j);
                        outimage[i, j] = new CPixel(color.R, color.G, color.B);
                    }
                }
                return outimage; //matriz de pixeles
            }
            return null;
        }

        /***********************************************************************************************************
         * Nombre de la función: MATRIZtoPNG
         * Funcionalidad: recibe una matriz de clase Pixel y un nombre de archivo y guarda la imagen en formato .png o .jpg
         * Parametros de entrada:
         *  --> inimage: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B que se visualizarán en la imagen a guardar.
         *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
         *              La posición [0,0] representa el valor de arriba a la izquierda y 
         *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
         *  --> filename: String que contiene el nombre con el que se guardará la imagen. Debe incluir el .png o .jpg
         *  Devuelve:
         *  --> True: Si se ha podido guardar la imagen
         *  --> False: Si no se ha podido guardar la imagen.
         ***********************************************************************************************************/
        static bool MATRIZtoPNG(CPixel[,] inimage, string filename) //De matriz inimage a .png
        {
            if (inimage != null && filename != null)
            {
                Bitmap outimage = new Bitmap(inimage.GetLength(0), inimage.GetLength(1));

                for (int i = 0; i < inimage.GetLength(0); i++) // Recorrido por la imagen para guardar los valores
                {
                    for (int j = 0; j < inimage.GetLength(1); j++)
                    {
                        outimage.SetPixel(i, j, Color.FromArgb(inimage[i, j].R, inimage[i, j].G, inimage[i, j].B));
                    }
                }
                outimage.Save("editada " + filename); // Guardar la imagen
                return true;
            }
            return false;
        }

        /***********************************************************************************************************
        FUNCIONES DE TXTaVECTOR / INICIO DE SESIÓN / REGISTER:
        * --> Login: Pide los datos al usuario y comprueba si existen.
        * --> TXTaVECTOR: Creamos un vector de vectores con datos de los usuarios.
        * --> Register: Pide los datos al usuario y los pone en un nuevo vector si no existe ya el usuario.
        * --> VECTORaTXT: Pasa el vector de vectores al archivo.
        ***********************************************************************************************************/

        static int Login(CLista lista_usuarios) // Mira si existe el usuario introducido en el archivo.
        {
            try
            {
                Console.WriteLine("Introduzca su nombre de usuario");
                string usuario = Console.ReadLine();
                Console.WriteLine("Introduzca su contraseña");
                string contraseña = Console.ReadLine();

                while(string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contraseña))
                {
                    Console.WriteLine("No escribas tonterias!!");
                    Console.WriteLine("Introduzca su nombre de usuario");
                    usuario = Console.ReadLine();
                    Console.WriteLine("Introduzca su contraseña");
                    contraseña = Console.ReadLine();
                }  
                
                for(int i = 0; i < 3333; i++)
                {
                    if(lista_usuarios.usuarios[i].nombre == usuario)
                    {
                        return 1; // existe el usuario, proceder con la edicion de fotos
                    }
                }
                return 2; //si user no existe, se tiene que registrar
            }
            catch(Exception)
            {
               return 0; // los datos no se han encontrado
            }
        }

        static int Register(CLista lista) //Pedimos nombre y contraseña al usuario y lo metemos en el archivo.
        {
            Console.WriteLine("Introduzca su nombre de usuario");
            string usuario = Console.ReadLine();
            Console.WriteLine("Introduzca su contraseña");
            string contraseña = Console.ReadLine();
            
            while(string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contraseña))
            {
                Console.WriteLine("No escribas tonterias!!");
                Console.WriteLine("Introduzca su nombre de usuario");
                usuario = Console.ReadLine();
                Console.WriteLine("Introduzca su contraseña");
                contraseña = Console.ReadLine();
            }

            for(int i = 0; i < 3333; i++)
            {
                if(lista.usuarios[i] == null)
                {
                    
                    lista.usuarios[i] = new CUsuario();
                }
                else if(lista.usuarios[i].nombre == usuario)
                {
                    lista.usuarios[i] = new CUsuario();
                    return 1; //user ya existe
                }
            }

            for(int j = 0; j < 5; j++)
            {
                if(string.IsNullOrWhiteSpace(lista.usuarios[j].nombre) || string.IsNullOrWhiteSpace(lista.usuarios[j].contraseña)) // miramos si hay algun hueco vacio
                {
                    lista.usuarios[j] = new CUsuario // se escriben los datos del usuario en el vector de vectores
                    {
                        nombre = usuario,
                        contraseña = contraseña
                    };
                    return 2; //podemos guardar el user en este espacio, proceder a escribir el user en ese vector y a la edicion de fotos
                }
            }
            return 0; //datos mal introducidos
        }
        
        static CLista TXTaVECTOR() 
        {   
            StreamReader leer = new StreamReader("usuarios.txt");
            CLista lista = new CLista();

            string linea = leer.ReadLine();
            for(int i = 0; linea != null; i++)
            {
                string[] trozos = linea.Split(' ');
                if(trozos.Length == 2)
                {
                    CUsuario usuario = new CUsuario
                    {
                        nombre = trozos[0],
                        contraseña = trozos[1]
                    };
                    lista.usuarios[i] = usuario;
                }
                linea = leer.ReadLine();
            }
            leer.Close();
            return lista;
        }

        static void VECTORaTXT(CLista lista_usuarios)
        {
            StreamWriter escritura  = new StreamWriter("usuarios.txt");

            for (int i = 0; i < lista_usuarios.usuarios.Length; i++)
            {
                escritura.WriteLine("{0} {1}", lista_usuarios.usuarios[i].nombre, lista_usuarios.usuarios[i].contraseña);
            }
            escritura.Close();
        }
        
        /***********************************************************************************************************
         * Nombre de la función: reverse
         * Funcionalidad: recibe una matriz de clase Pixel y devuelve una matriz de clase Pixel con los valores espejo.
         * Parametros de entrada:
         *  --> inimage: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B.
         *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
         *              La posición [0,0] representa el valor de arriba a la izquierda y 
         *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.         
         *  Devuelve:
         *  --> Matriz de clase Pixel. Cada Pixel contiene los valores R, G y B.
         *              La forma de la matriz es la misma que la matriz de entrada
         *              La posición [0,0] representa el valor de arriba a la derecha de la matriz original y 
         *              la posición [anchura,altura] el valor de abajo a la izquierda de la matriz original
         *  --> null: Si no se ha pasado un parámetro de entrada != null.
         ***********************************************************************************************************/
        static CPixel[,] reverse(CPixel[,] inimage)
        {
            CPixel[,] result = null;
            int width, height;

            if (inimage != null)
            {
                width = inimage.GetLength(0);
                height = inimage.GetLength(1);
                result = new CPixel[width, height];

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                    // igualar al lado opuesto
                    result[i, j] = inimage[width - i - 1, j];
                    }
                }
                return result;
            }
            return result;
        }

        /***********************************************************************************************************
          * Nombre de la función: enmarcar
          * Funcionalidad: 
          * Parametros de entrada:
          *  --> inimage: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B que se visualizarán en la imagen a guardar.
          *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
          *              La posición [0,0] representa el valor de arriba a la izquierda y 
          *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
          *  Devuelve:
          *  --> Matriz de pixeles
          ***********************************************************************************************************/
        static CPixel[,] enmarcar(CPixel[,] inimage)
        {
            CPixel[,] result = null;
            if (inimage != null)
            {
                int width = inimage.GetLength(0);
                int height = inimage.GetLength(1);
                int j, i;
                result = inimage;
                CPixel colorRojo = new CPixel(155, 100, 50);

                for (i = 0; i < width; i++)
                {
                    for (j = 0; j < height; j++)
                    {
                        if (i <= 10 || i >= width - 11 || j <= 10 || j >= height - 11) // verificamos si estamos en donde queremos enmarcar
                        {
                            result[i, j] = new CPixel(colorRojo.R, colorRojo.G, colorRojo.B);
                            //Hacer que calcule la altura de la imagen y que el marco opcupe el 20% de los pixeles de la altura
                        }
                    }
                }
                return result;
            }
            return result;
        }

        /***********************************************************************************************************
         * Nombre de la función: cambiocolor
         * Funcionalidad: Cambiar el valor de los pixeles de la foto
         * Parametros de entrada:
         *  --> inimage: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B que se visualizarán en la imagen a guardar.
         *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
         *              La posición [0,0] representa el valor de arriba a la izquierda y 
         *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
         *  --> color: rojo, verde o azul
         *  Devuelve:
         *  --> matriz result con los pixeles cambiados
         ***********************************************************************************************************/
        static CPixel[,] cambiocolor(CPixel[,] inimage, string color)
        {
            CPixel[,] result = null;

            if (inimage != null)
            {
                int width, height, rojo, verde, azul;
                width = inimage.GetLength(0);
                height = inimage.GetLength(1);
                result = inimage;
                //inimage = new CPixel[width, height]; //hace falta tio?

                if(color == "rojo")
                {
                    for(int i  = 0; i < width; i++)
                    {
                        for(int j = 0;j < height; j++)
                        {
                            CPixel pix = result[i, j];
                            
                            rojo = pix.R;
                            verde = pix.G;
                            azul = pix.B;

                            if (rojo <= 205)
                            {
                                result[i, j] = new CPixel(rojo + 50, verde, azul);
                            }
                        }
                    }
                }
                if (color == "verde")
                {
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            CPixel pix = result[i, j];

                            rojo = pix.R;
                            verde = pix.G;
                            azul = pix.B;

                            if (verde <= 205)
                            {
                                result[i, j] = new CPixel(rojo, verde + 50, azul);
                            }
                        }
                    }
                }
                if (color == "azul")
                {
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            CPixel pix = result[i, j];

                            rojo = pix.R;
                            verde = pix.G;
                            azul = pix.B;

                            if (azul <= 205)
                            {
                                result[i, j] = new CPixel(rojo, verde, azul + 50);
                            }
                        }
                    }
                }
                return result;
            }
            return result;
        }

        /***********************************************************************************************************
         * Nombre de la función: collage
         * Funcionalidad: Crear un collage con cuatro fotos
         * Parametros de entrada:
         *  --> inimage: Matriz de clase Pixel. Cada Pixel debe contener los valores R, G y B que se visualizarán en la imagen a guardar.
         *              La forma de la matriz debe ser [anchura, altura], donde anchura y altura son el tamaño de la imagen en los ejes X e Y.
         *              La posición [0,0] representa el valor de arriba a la izquierda y 
         *              la posición [anchura,altura] el valor de abajo a la derecha de la imagen original.
         *  --> inimage2
         *  --> inimage3
         *  --> inimage4
         *  Devuelve:
         *  --> la matriz resultado
         ***********************************************************************************************************/
        public static CPixel[,] collage(CPixel[,] inimage, CPixel[,] inimage2, CPixel[,] inimage3, CPixel[,] inimage4)
        {
            CPixel[,] result = null;

            if (inimage != null || inimage2 != null || inimage3 != null || inimage4 != null)
            {
                // Verifica que las dimensiones de todas las imágenes sean iguales
                int height = inimage.GetLength(0);
                int width = inimage.GetLength(1);

                if (height == inimage2.GetLength(0) && width == inimage2.GetLength(1) &&
                    height == inimage3.GetLength(0) && width == inimage3.GetLength(1) &&
                    height == inimage4.GetLength(0) && width == inimage4.GetLength(1))
                {
                    result = new CPixel[height * 2, width * 2];

                    // Combinar las imágenes en el collage
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            result[i, j] = inimage[i, j];
                            result[i + width, j] = inimage2[i, j];
                            result[i, j + height] = inimage3[i, j];
                            result[i + width, j + height] = inimage4[i, j];
                        }
                    }
                    return result;
                }
            }
            return result;
        }

        // Comprueba si la primera foto es cuarada.
        public static bool check_if_cuadrada(CPixel[,] inimage) 
        {
            if(inimage.GetLength(0) == inimage.GetLength(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Comprueba si las cuatro imagenes son cuadradas.
        public static bool todas_cuadradas(CPixel[,] inimage, CPixel[,] inimage2, CPixel[,] inimage3, CPixel[,] inimage4)
        {
            if(inimage.GetLength(0) == inimage.GetLength(1) && inimage2.GetLength(0) == inimage2.GetLength(1) 
              && inimage3.GetLength(0) == inimage3.GetLength(1) && inimage4.GetLength(0) == inimage4.GetLength(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /***********************************************************************************************************
         * Nombre de la función: menu y minimeu
         * Funcionalidad: Mostrar al usuario las opciones que tiene disponibles y sus respectivos codigos
         * Devuelve:
         *  --> 1 linea en la consola que pregunta qué operacion se quiere realizar
         *  --> 5 líneas en el caso de menu y 3 en el minimenu en la consola, cada una con una operacion realizable
         ***********************************************************************************************************/
        static void menu()
        {
            Console.WriteLine();
            Console.WriteLine("*********************************************************************************");
            Console.WriteLine(" Qué operación quieres realizar?");
            Console.WriteLine();
            Console.WriteLine(" 0: Terminar");
            Console.WriteLine();
            Console.WriteLine(" 1: Enmarcar la foto");
            Console.WriteLine();
            Console.WriteLine(" 2: Hacer un collage con 4 fotos");
            Console.WriteLine();
            Console.WriteLine(" 3: Cambiar la tonalidad de la foto");
            Console.WriteLine();
            Console.WriteLine(" 4: Invertir la imagen");
            Console.WriteLine("*********************************************************************************");
            Console.WriteLine();
        }

        static void minimenu()
        {
            Console.WriteLine();
            Console.WriteLine("*********************************************************************************");
            Console.WriteLine("Elije: ");
            Console.WriteLine("0. Salir");
            Console.WriteLine("1. Iniciar sesión");
            Console.WriteLine("2. Registrarse");
            Console.WriteLine("*********************************************************************************");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            string fotoelegida, fotoelegida2, fotoelegida3, fotoelegida4, colorelegido;
            string usuario, contraseña, opcion;
            int opcion_minimenu = 1000;
            bool guardada;
            bool salir = false;
            CPixel[,] inimage2, inimage3, inimage4;
            
            CLista lista = new CLista();
            
            try //se gestiona la no localizacion del archivo usuarios.txt
            {
                StreamReader lectura = new StreamReader("usuarios.txt");
                lectura.Close();
            }
            catch(FileNotFoundException)    
            {      
                Console.WriteLine("Aun no existe el archivo usuarios.txt o se ha alterado su nombre, vamos a crear otro y ya esta");
                StreamWriter creartxt = new StreamWriter("usuarios.txt");
                creartxt.Close();
            }

            lista = TXTaVECTOR();
            
            Console.WriteLine("Buenas!! Qué desea hacer? Introduzca el numero a la izquierda de la operacion: ");
            minimenu();
            string opcion_usuario = Console.ReadLine();

            while(opcion_usuario != "0" && !salir)
            {
                switch(opcion_usuario)
                { 
                    case "0":
                        Console.WriteLine("Saliendo chavalin");
                        break;

                    case "1":
                        Console.WriteLine("Entrando al iniciando sesión...");
                        int login = Login(lista);
                        if(login == 1)
                        {
                            Console.WriteLine("Bienvenido de nuevo guapete");
                            opcion_minimenu = 1;
                            salir = true;
                        }
                        else if(login == 0)
                        {
                            Console.WriteLine("Datos no encontrados");
                            minimenu();
                            opcion_usuario = Console.ReadLine();
                        }
                        else if(login == 2)
                        {
                            Console.WriteLine("No existes... Te tienes que registrar primero");
                            minimenu();
                            opcion_usuario = Console.ReadLine();
                        }
                        break;

                    case "2":
                        Console.WriteLine("Entrando al registro...");
                        int register = Register(lista);
                        if(register == 1)
                        {
                            Console.WriteLine("Este usuario ya existe, si quieres inicia sesión");
                            minimenu();
                            opcion_usuario = Console.ReadLine();
                        }
                        else if(register == 2)
                        {
                            Console.WriteLine("Bienvenido al editor de fotos, has sido guardado");
                            salir = true;
                            opcion_minimenu = 2;
                        }
                        else if(register == 0)
                        {
                            Console.WriteLine("Datos mal introducidos");
                            minimenu();
                            opcion_usuario = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Vaya vaya tío, error inesperado, mira que es raro que pase pero vas y lo consigues");
                            minimenu();
                            opcion_usuario = Console.ReadLine();
                        }
                        break;

                    default:
                        Console.WriteLine("Codigo incorrecto chavalín... elije de nuevo");
                        minimenu();
                        opcion_usuario = Console.ReadLine();
                        break;
                }
            } 

            if(opcion_minimenu == 1 || opcion_minimenu == 2)
            {       
            
                Console.WriteLine("Qué imagen quieres manipular? introduce el nombre y el .png/.jpg");
                fotoelegida = Console.ReadLine();
                CPixel[,] inimage;

                bool existe = false;
                while (!existe)
                {
                    try // Probamos si existe la imagen
                    {                    
                        inimage = PNGtoMATRIZ(fotoelegida);
                        existe = true; // Si no hay excepción, establecemos que existe a true para salir del bucle
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Error al cargar la imagen, introduce nombre de imagen de nuevo:");
                        existe = false;
                        fotoelegida = Console.ReadLine();
                    }
                }

                inimage = PNGtoMATRIZ(fotoelegida);

                if (inimage != null)
                {
                    menu();
                    opcion = Console.ReadLine();

                    while (opcion != "0")
                    {
                        switch (opcion)
                        {
                            case "0":
                                Console.WriteLine("Terminando Programa");
                                break;

                            case "1":
                                Console.WriteLine("Enmarcando de color rarito (155, 100, 50)...");
                                enmarcar(inimage);
                                if (enmarcar(inimage) != null)
                                {
                                    guardada = MATRIZtoPNG(enmarcar(inimage), fotoelegida);
                                    if (guardada == true)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Foto guardada tio");
                                    }
                                }
                                break;

                            case "2":
                                Console.WriteLine();
                                Console.WriteLine("Comprobando que la primera foto sea cuadrada...");
                                bool compr = check_if_cuadrada(inimage);

                                if(compr == true)
                                {
                                    Console.WriteLine("La foto es cuadrada :))))");
                                    Console.WriteLine();
                                    Console.WriteLine("Para el collage necesito 3 fotos más tío (tienen que ser cuadradas)");
                                    
                                    existe = false;
                                    Console.WriteLine("Por favor ingrese la segunda foto: ");
                                    fotoelegida2 = Console.ReadLine();
                                    Console.WriteLine("Por favor ingrese la tercera foto: ");
                                    fotoelegida3 = Console.ReadLine();
                                    Console.WriteLine("Por favor ingrese la cuarta foto: ");
                                    fotoelegida4 = Console.ReadLine();

                                    while(true)
                                    {                                    
                                        try
                                        {
                                            inimage2 = PNGtoMATRIZ(fotoelegida2);
                                            inimage3 = PNGtoMATRIZ(fotoelegida3);
                                            inimage4 = PNGtoMATRIZ(fotoelegida4);
                                            break;
                                        }
                                        catch(ArgumentException)
                                        {
                                            Console.WriteLine("No existe alguna de las imagenes");
                                        }
                                        Console.WriteLine("Por favor ingrese la segunda foto: ");
                                        fotoelegida2 = Console.ReadLine();
                                        Console.WriteLine("Por favor ingrese la tercera foto: ");
                                        fotoelegida3 = Console.ReadLine();
                                        Console.WriteLine("Por favor ingrese la cuarta foto: ");
                                        fotoelegida4 = Console.ReadLine();
                                    }
                                    inimage2 = PNGtoMATRIZ(fotoelegida2);
                                    inimage3 = PNGtoMATRIZ(fotoelegida3);
                                    inimage4 = PNGtoMATRIZ(fotoelegida4);

                                    Console.WriteLine("Vamos a comprobar si son cuadradas...");
                                    
                                    bool comprobar = todas_cuadradas(inimage, inimage2, inimage3, inimage4);
                                    if(comprobar == true)
                                    {
                                        Console.WriteLine("Perfecto, son cuadradas");
                                        Console.WriteLine();
                                        Console.WriteLine("Vamos a empezar con el collage sisisiiss...");

                                        if(collage(inimage, inimage2, inimage3, inimage4) != null)
                                        {
                                            string nom = "collage.png";
                                            CPixel[,] result = collage(inimage, inimage2, inimage3, inimage4);
                                            guardada = MATRIZtoPNG(result, nom);
                                            if(guardada == true)
                                            {
                                                Console.WriteLine("Foto guardada tio");
                                            }
                                            else
                                            {
                                                Console.WriteLine("No se ha guardado joder");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("La foto dada no es cuadrada por lo tanto no se puede hacer el collage, ingrese otra operación");
                                }
                                break;
                                
                            case "3":
                                Console.WriteLine("¿Quieres cambiar a rojo, verde o azul?: ");
                                colorelegido = Console.ReadLine().ToLower();

                                if(colorelegido == "rojo" || colorelegido ==  "verde" || colorelegido ==  "azul")
                                {
                                    Console.WriteLine("Perfecto, " + colorelegido + ", manos a la obra");
                                    cambiocolor(inimage, colorelegido);
                                    if (cambiocolor(inimage, colorelegido) != null)
                                    {
                                        guardada = MATRIZtoPNG(cambiocolor(inimage, colorelegido), fotoelegida);
                                        if (guardada == true)
                                        {
                                            Console.WriteLine("Foto guardada tio");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error al elegir un color, Albert, hay que teclear o rojo o verde o azul...");
                                }
                                break;

                            case "4":
                                Console.WriteLine("Espera un segundín mientras invierto la foto...");
                                reverse(inimage);
                                if (reverse(inimage) != null)
                                {
                                    guardada = MATRIZtoPNG(reverse(inimage), fotoelegida);
                                    if (guardada == true)
                                    {
                                        Console.WriteLine("Foto guardada tio");
                                    }
                                }
                                break;

                            default:
                                Console.WriteLine("Código de operacion incorrecto chavalín");
                                break;
                        }
                        menu();
                        opcion = Console.ReadLine();
                    }
                    Console.WriteLine("Guardando usuarios en en el archivo bro...");
                    VECTORaTXT(lista);
                }
                else
                {
                    Console.WriteLine("No se pudo cargar la imagen jodeeeeeer");
                }
            }
            else
            {
                Console.WriteLine("Saliendo...");
            }
            Console.WriteLine("Hasta otra tío! :D No olvides suscribirte, like, la campanita y comentar qué te pareció");
        }
    }
}