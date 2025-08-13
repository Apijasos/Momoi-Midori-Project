//Librerias que permiten manejar el motor (Godot) y el lenguaje (C#).
using Godot;
using System;

public partial class Player : Node2D //=> Clase pública declarada, partimos de Player y 'heredamos' la clase Node2D
{
	
	//PD: Al final de cada linea de código se cierra con ';'. Las funciones, métodos y statements (bucles, ifs, etc.) se abren y cierran con '{}'
	
	//Hambre y su valor máximo en una variable aparte. Se pueden cambiar en el inspector de Player al tener '[Export]'
	[Export] public int HambreMax  {get; set;} = 100; 
	[Export] public int Hambre {get; set;} = 80;
	[Export] public int RelacionMax {get; set;} = 100;
	[Export] public int Relacion {get; set;} = 50;
	[Export] public int CorduraMax {get; set;} = 100;
	[Export] public int Cordura {get; set;} = 21;
	[Export] public int ChoiceMax {get; set;} = 4;
	[Export] public int Choice {get; set;} = 4;
	[Export] public int Day {get; set;} = 1;
		
	//Variable para generar un numero aleatorio y usarlo en todo el programa
	private RandomNumberGenerator rng;
	
	//Variable que guarda el indice [i] del vector de los dialogos. Evita dos (o más) repeticiones de dialogo.
	private int IndiceDialogoAnterior = -1;
	
	//Inicialización de variables, en este caso solo el de random, ya que afecta otras funciones
	public override void _Ready()
	{
		rng = new RandomNumberGenerator();
		rng.Randomize();
	}
	
	/*Función que trabaja con la lógica de la exploración. Devuelve un bool (si encontró o no comida) 
	y un int (que especifica cuánta cómida se encontro)*/
	public (bool, int) Explorar()
	{
		//Una lógica de "tirada de dados", donde se trabajan las posibilidades del éxito de la exploración
		int Roll = (int)rng.RandiRange(0, 100); // => Generamos un entero que devuelva un valor entre 0 y 100
		int ChanceBase = 60; // => Chances de que no se pueda encontrar comida. Disminuirlo aumenta la dificultad.
		ContadorDecisiones(); //=> Se llama la funcion para restar una decision al jugador
		//Si el entero generado es menor que las chances iniciales la exploración fue un éxito.
		if (Roll < ChanceBase)
		{
			int ComidaEncontrada = (int)rng.RandiRange(8, 25); //=> Un número entre el 8 y el 25 que dirá cuanta comida encontraste
			Hambre = Math.Min(HambreMax, Hambre + ComidaEncontrada); /*=> A nuestra hambre le agregamos la comida
			
																	Y con Math.Min redondeamos al entero más chico*/
			return(true, ComidaEncontrada); //=> Devolvemos el resultado (true = si se encontró comida, ComidaEncontrada = valor hallado)
		}
		//Si no...
		else
		{
			int Costo = (int)rng.RandiRange(2, 6); //=> Chavito, el viaje a Acapulco no fue gratis... (cuanta hambre nos costo)
			Hambre = Math.Max(0, Hambre - Costo); //=> A nuestra hambre actual le restamos lo que nos costó. Math.Max devuelve el entero redondeado más alto.
			int Locura = (int)rng.RandiRange(5, 10);
			Cordura = Math.Max(0, Cordura - Locura);
			return(false, 0);// => Devolvemos el resultado (false = no se encontró comida, 0 = valor hallado)
		}
		/*PD: Si el hambre sube es "bueno", ya que estariamos comiendo.
			  Si disminuye es "malo", ya que no estariamos comiendo. No se compliquen tanto en esta lógica.*/ 
	}
	
	//Función que devuelve un string a la hora de jugar con la mina. Jugar cuesta hambre.
	public string Jugar()
	{
		int Costo = (int)rng.RandiRange(3, 6);//=> Calculamos cuanto nos costó jugar
		Hambre = Math.Max(0, Hambre - Costo); //=> Lo que nos costó lo restamos del hambre
		ContadorDecisiones(); //=> Se llama la funcion para restar una decision al jugador
		int CorduraGanada = (int)rng.RandiRange(5, 20);
		Cordura = Math.Max(0, CorduraGanada + Cordura);
		return $"Jugaron y se divirtieron jijo. Jugar costó {Costo} de hambre";//=> Devolvemos el string con los datos
	}
	
	//Función que devuelve un string a la hora de hablar con la mina.
	public string Hablar()
	{
		string[] Dialogos = new string[]{ //=> Un vector de una dimensión que almacena dialogos.
			"Holii",
			"Desearía poder volver a casa...",
			"¿Cómo estás?",
			"Moxas me debe plata...",//Esto es una versión arcaica. Se pasará todo a una base de datos
			"¿Maxi? ah, ya te recuerdo...",
			"Kevin, ¿dónde estabas cuando te necesitaba?",
			"Escocia... aún me pregunto por qué...",
			"Me divierto mucho con vos.", //La base de datos estará hecha con SQLite 
			"No creo haberte dicho que pudieras hablarme.",
			"Perón me quito muchas cosas...",//Hay una pequeña versión en los archivos locales de este proyecto
			"Ahora que lo pienso, le debo mi vida al general.",//Se llama "DB_Game.dbb.
			"Un otaku, wtf...",
			"Matate y grabalo.",//La conexión a esta se hará con un NuGet (libreria) ya instalada por el Api.
			"Daffcan... hace mucho no escuchaba ese nombre...",
			"¿Que querés qué?",
			"Extraño estar con vos...",
			"Estoy aún en desarrollo :3",
		}; 
		int i; //=> Indice para posicionarnos en el vector
		do //=> Lógica 'do - while' para generar un indice distinto al anterior, y evitar repeticiones seguidas 
		{
			i = (int)rng.RandiRange(0, Dialogos.Length - 1);//PD .RandiRange(int, int) te devuelve un número aleatorio entre un rango de dos enteros
		}
		while(i == IndiceDialogoAnterior && Dialogos.Length > 0);//Hacer el Do mientras el indice sea igual al indice anterior, guardado globalmente con anterioridad
		
		int interaccion = (int)rng.RandiRange(5, 10);
		Relacion = Math.Max(0, Relacion + interaccion);
		
		IndiceDialogoAnterior = i; //Cuándo se rompe el bucle se guarda el indice nuevo en está variable, para que la próxima vez no se repita este dialogo
		ContadorDecisiones(); //=> Se llama la funcion para restar una decision al jugador
		return Dialogos[i]; //Devolvemos el string. Especificamos el indice para no devolver un vector.
		
		}
		
		// Funcion que le brinda al jugador la cantidad de decisiones que puede ejecutar por dia
		public void ContadorDecisiones() {
			Choice--;
			if(Choice==0){
				Choice=4;
				Day++;
			}
		}
}
