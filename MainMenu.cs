using Godot;
using System;

//MENÚ PRINCIPAL DEL JUEGO

public partial class MainMenu : Control
{
	// No hace falta exportar los NodePath si buscas por nombre fijo
	private VBoxContainer ControladoresPrincipales;
	private Button BtnJugar, BtnCargarPartida, BtnOpciones, BtnSalir, BtnCargarPartida1, BtnCargarPartida2;
	private Panel PnlOpciones, PnlCargarPartida;
	private Button BtnOpcion1, BtnOpcion2;
	private Label LblOpciones, LblCargarPartida, LblVolumen;
	private HSlider VolumeSlider;
	private AudioStreamPlayer AudioStreamPlayer;
	
	//Constructor de los anteriores atributos, y reasignación del método .Pressed() de dos de los cuatro botones (por ahora)
	public override void _Ready()
	{
		// Inicialización usando los nombres exactos del árbol
		ControladoresPrincipales = GetNode<VBoxContainer>("ControladoresPrincipales");
		BtnJugar = ControladoresPrincipales.GetNode<Button>("BtnJugar");
		BtnCargarPartida = ControladoresPrincipales.GetNode<Button>("BtnCargarPartida");
		BtnOpciones = ControladoresPrincipales.GetNode<Button>("BtnOpciones");
		BtnSalir = ControladoresPrincipales.GetNode<Button>("BtnSalir");

		PnlOpciones = GetNode<Panel>("PnlOpciones");
		BtnOpcion1 = PnlOpciones.GetNode<Button>("BtnOpcion1");
		BtnOpcion2 = PnlOpciones.GetNode<Button>("BtnOpcion2");
		LblOpciones = PnlOpciones.GetNode<Label>("LblOpciones");
		VolumeSlider = PnlOpciones.GetNode<HSlider>("VolumeSlider");

		PnlCargarPartida = GetNode<Panel>("PnlCargarPartida");
		BtnCargarPartida1 = PnlCargarPartida.GetNode<Button>("BtnCargarPartida1");
		BtnCargarPartida2 = PnlCargarPartida.GetNode<Button>("BtnCargarPartida2");
		LblCargarPartida = PnlCargarPartida.GetNode<Label>("LblCargarPartida");

		LblVolumen = PnlOpciones.GetNode<Label>("LblVolumen");
		AudioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

		// Conectar eventos de botones
		BtnJugar.Pressed += PresionarJugar;
		BtnCargarPartida.Pressed += PresionarCargarPartida;
		BtnOpciones.Pressed += PresionarOpciones;
		BtnSalir.Pressed += PresionarSalir;
		BtnOpcion1.Pressed += _on_btn_opcion_1_pressed;
		BtnOpcion2.Pressed += _on_btn_opcion_2_pressed;
		BtnCargarPartida1.Pressed += _on_btn_cargar_partida_1_pressed;
		BtnCargarPartida2.Pressed += _on_btn_cargar_partida_2_pressed;
		VolumeSlider.ValueChanged += _on_VolumeSlider_value_changed;

		// Inicialización visual
		PnlOpciones.Visible = false;
		PnlCargarPartida.Visible = false;

		// Inicialización del slider de volumen y audio
		VolumeSlider.Value = 100;
		_on_VolumeSlider_value_changed(VolumeSlider.Value);

		// Debug
		GD.Print("Inicialización completa del menú principal.");
	}
	
	//Metodo para abrir la escena principal del juego
	private void PresionarJugar() => GetTree().ChangeSceneToFile("res://Main.tscn");
	
	//Metodo para salir, si, es una boludez, podes simplemente asignarlo así: 'BtnSalir.Pressed += GetTree().Quit(); pero lo usamos de base para agregar otras cosas, como un cuadro de dialogo de confirmación
	private void PresionarSalir() => GetTree().Quit();
	
	private void PresionarOpciones()
	{
		if(PnlOpciones.Visible==false)
		{
			PnlOpciones.Visible = true; 
			PnlCargarPartida.Visible = false;
		} 
		else 
		{
			PnlOpciones.Visible = false; 
			PnlCargarPartida.Visible = false;
		}
	}
	
	private void PresionarCargarPartida() 
	{
		if(PnlCargarPartida.Visible==false) 
		{
			PnlCargarPartida.Visible = true; 
			PnlOpciones.Visible = false;
		} 
		else
		{
			PnlCargarPartida.Visible = false; 
			PnlOpciones.Visible = false;
		}
	}
	
	private void _on_btn_opcion_1_pressed() => PnlOpciones.Visible = false;
	
	private void _on_btn_opcion_2_pressed() => PnlOpciones.Visible = false;
	
	private void _on_VolumeSlider_value_changed(double value)
	{
		float minDb=-80f;
		float maxDb=0f;
		
		float sliderValue=(float)value;
		
		float volumeDb;
		
		if(sliderValue <= 0){
			volumeDb = minDb; // Silencio
		} else {
		volumeDb = Mathf.Lerp(minDb, maxDb, (float)Math.Log10(sliderValue / 100f * 9 + 1));
		}
		
		AudioStreamPlayer.VolumeDb = volumeDb;
	}
	
	private void _on_btn_cargar_partida_1_pressed() => PnlCargarPartida.Visible = false;
	
	private void _on_btn_cargar_partida_2_pressed() => PnlCargarPartida.Visible = false;
}
	
