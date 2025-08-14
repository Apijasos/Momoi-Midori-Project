using Godot;
using System;

//MENÚ PRINCIPAL DEL JUEGO

public partial class MainMenu : Control
{
	//Path hacía el VBoxContainer de ControladoresPrincipales (se tiene que asignar en el inspector de la derecha)
	[Export] private NodePath ControladoresPrincipales_path;
	[Export] private NodePath PnlOpciones_path;
	[Export] private NodePath PnlCargarPartida_path;
	
	//Los controladores gráficos, y el VBoxContainer, declarados como variables
	private VBoxContainer ControladoresPrincipales, ControladoresCargarPartida, ControladoresOpciones;
	private Button BtnJugar, BtnCargarPartida, BtnOpciones, BtnSalir, BtnOpcion1, BtnOpcion2, BtnCargar1, BtnCargar2;
	private Label LblOpcion, LblCargarPartida;
	private Panel PnlOpciones, PnlCargarPartida;
	
	//Constructor de los anteriores atributos, y reasignación del método .Pressed() de dos de los cuatro botones (por ahora)
	public override void _Ready()
	{
		//Asignación de los paths de los controladores gráficos. El primero a través del inspector y el resto escribiendo su dirección con los nodos
		ControladoresPrincipales = GetNode<VBoxContainer>(ControladoresPrincipales_path);
		PnlOpciones = GetNode<Panel>(PnlOpciones_path);
		PnlCargarPartida = GetNode<Panel>(PnlCargarPartida_path);
		
		ControladoresOpciones = GetNode<VBoxContainer>("PnlOpciones/ControladoresOpciones");
		ControladoresCargarPartida = GetNode<VBoxContainer>("PnlCargarPartida/ControladoresCargarPartida"); 
		BtnJugar = GetNode<Button>("ControladoresPrincipales/BtnJugar");
		BtnCargarPartida = GetNode<Button>("ControladoresPrincipales/BtnCargarPartida");
		BtnOpciones = GetNode<Button>("ControladoresPrincipales/BtnOpciones");
		BtnSalir = GetNode<Button>("ControladoresPrincipales/BtnSalir");
		BtnOpcion1 = GetNode<Button>("ControladoresOpciones/BtnOpcion1");
		BtnOpcion2 = GetNode<Button>("ControladoresOpciones/BtnOpcion2");
		BtnCargar1 = GetNode<Button>("ControladoresCargarPartida/BtnOpcion1");
		BtnCargar2 = GetNode<Button>("ControladoresCargarPartida/BtnOpcion2");
		
		//Declaro que pasa cuando se presionan BtnJugar y BtnSalir
		BtnJugar.Pressed += PresionarJugar;
		BtnSalir.Pressed += PresionarSalir;
		BtnOpciones.Pressed += PresionarOpciones;
		BtnCargarPartida.Pressed += PresionarCargarPartida;
		
		PnlOpciones.Visible = false;
		PnlCargarPartida.Visible = false;
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
	
	private void PresionarOpciones()
	{
		PnlOpciones.Visible = true;
		
	}
	
	private void PresionarCargarPartida()
	{ 	
		PnlCargarPartida.Visible = true;
	}
	
	private void _on_btn_opcion_1_pressed()
	{
		PnlOpciones.Visible = false;
	}
	
	private void _on_btn_opcion_2_pressed()
	{
		PnlOpciones.Visible = false;
	}
	
	private void _on_btn_cargar_partida_1_pressed()
	{
		PnlCargarPartida.Visible = false;
	}
	
	private void _on_btn_cargar_partida_2_pressed()
	{
		PnlCargarPartida.Visible = false;
	}
}
	
