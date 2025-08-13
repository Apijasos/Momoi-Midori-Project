using Godot;
using System;

//MENÚ PRINCIPAL DEL JUEGO

public partial class MainMenu : Control
{
	//Path hacía el VBoxContainer de ControladoresPrincipales (se tiene que asignar en el inspector de la derecha)
	[Export] private NodePath ControladoresPrincipales_path;
	
	//Los controladores gráficos, y el VBoxContainer, declarados como variables
	private VBoxContainer ControladoresPrincipales;
	private Button BtnJugar, BtnCargarPartida, BtnOpciones, BtnSalir;
	
	//Constructor de los anteriores atributos, y reasignación del método .Pressed() de dos de los cuatro botones (por ahora)
	public override void _Ready()
	{
		//Asignación de los paths de los controladores gráficos. El primero a través del inspector y el resto escribiendo su dirección con los nodos
		ControladoresPrincipales = GetNode<VBoxContainer>(ControladoresPrincipales_path);
		BtnJugar = GetNode<Button>("ControladoresPrincipales/BtnJugar");
		BtnCargarPartida = GetNode<Button>("ControladoresPrincipales/BtnCargarPartida");
		BtnOpciones = GetNode<Button>("ControladoresPrincipales/BtnOpciones");
		BtnSalir = GetNode<Button>("ControladoresPrincipales/BtnSalir");
		
		//Declaro que pasa cuando se presionan BtnJugar y BtnSalir
		BtnJugar.Pressed += PresionarJugar;
		BtnSalir.Pressed += PresionarSalir;
	}
	
	//Metodo para abrir la escena principal del juego
	private void PresionarJugar()
	{
		GetTree().ChangeSceneToFile("res://Main.tscn");
	}
	
	//Metodo para salir, si, es una boludez, podes simplemente asignarlo así: 'BtnSalir.Pressed += GetTree().Quit(); pero lo usamos de base para agregar otras cosas, como un cuadro de dialogo de confirmación
	private void PresionarSalir()
	{
		GetTree().Quit();
	}
	/*
	Agregar un menú principal.
	
	En este nuevo nodo, llamado "MainMenu", vas a crear el menú principal.
	
	¿Cómo lo vas a hacer?
	
	Primero vas a agregarle los nodos hijos correspondientes para el menú.
	Este tendrá un fondo y cuatro botones. Un botón "Jugar", otro "Cargar Partida", uno llamado "Opciones"
	y por último "Salir". Por ahora solo programarás la lógica de "Jugar" y "Salir", que son simples.
	
	Arriba a la izquierda, en el visualizador de Escena vas a apretar click derecho en el nodo "MainMenu", y agregas:
	- VBoxContainer (Lo llamarás 'VBoxBotones')
	- Button (cuatro de estos, se llamarán 'BtnJugar', 'BtnCargarPartida', 'BtnOpciones', y 'BtnSalir')
	
	A los cuatro botones los arrastras hacia el VBoxContainer, asi son nodos hijos de este.
	
	Cómo tal, Jugar abre otra escena, lo mismo pasará con Cargar Partida y Opciones (menos Salir, que sale del juego)
	
	En MainMenu.cs vamos a hacer que el NodePath lo podas asignar desde el inspector (menú de la izquierda).
	La declaración de atributos será la siguiente: Cuatro [Export] que guarden la dirección del nodo de los cuatro botones
	y vas a guardar los cuatro botones como atributos (Ej: 'private button BtnJugar;' y para el export '[Export] private NodePath BtnJugar_Path')
	
	En el constructor _Ready() vas a asignar los cuatro botones, declarados como atributos, a sus respectivos nodepaths
	RECORDA que ahora la dirección del nodo está en el inspector, asi que a cada uno le vas a arrastrar, desde el menú
	de la izquierda hacia el inspector (Ej: Vas a arrastrar BtnJugar desde la izquierda hacia BtnJugar_Path del inspector, y así con el resto)
	Ejemplo de asignación de botones a nodepaths: BtnJugar = GetNode<Button>(BtnJugar_Path);.
	
	Luego le vas a asignar, a las funciones integradas de .Pressed de los botones cuatro métodos, que serán la lógica de que hace cada uno
	a BtnJugar.Pressed le vas a asignar un método llamado, digamos, 'PresionarJugar', que este abrirá el menú principal.
	Y así con el resto.
	
	PD: Para hacer un método es así: 
	private void Nombre()
	{
		(Lógica)
	}
	
	Recorda que para asignar esos metodos a .Pressed de los botones vas a primero tener que hacer los métodos y
	después volver a asignarlos en el _Ready()
	
	PD2: Recorda que vas a tener que acomodar los controladores, estos van a estar en el menú 2D (de arriba).
	
	Cualquier cosa me hablas al Discord. Éxitos :)
	*/
}
