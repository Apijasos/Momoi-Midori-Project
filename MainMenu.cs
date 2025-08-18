using Godot;
using System;

//MENÚ PRINCIPAL DEL JUEGO

public partial class MainMenu : Control
{
	// No hace falta exportar los NodePath si buscas por nombre fijo
	private VBoxContainer ControladoresPrincipales;
	private Button BtnJugar, BtnOpciones, BtnSalir, BtnCargarPartida1, BtnCargarPartida2, 
	BtnOpcion1, BtnOpcion2, BtnNuevoJuego;
	private Panel PnlOpciones, PnlJuego;
	private Label LblOpciones, LblCargarPartida, LblVolumen, LblNuevoJuego, LblBrillo;
	private HSlider VolumeSlider, GlowSlider;
	private AudioStreamPlayer AudioStreamPlayer;
	private CanvasModulate GlowCanvas;
	private ColorRect FadeRect;
	//Constructor de los anteriores atributos, y reasignación del método .Pressed() de dos de los cuatro botones (por ahora)
	public override void _Ready()
	{
		// Inicialización usando los nombres exactos del árbol
		ControladoresPrincipales = GetNode<VBoxContainer>("ControladoresPrincipales");
		BtnJugar = ControladoresPrincipales.GetNode<Button>("BtnJugar");
		BtnOpciones = ControladoresPrincipales.GetNode<Button>("BtnOpciones");
		BtnSalir = ControladoresPrincipales.GetNode<Button>("BtnSalir");
		
		PnlJuego = GetNode<Panel>("PnlJuego");
		BtnNuevoJuego = PnlJuego.GetNode<Button>("BtnNuevoJuego");
		BtnCargarPartida1 = PnlJuego.GetNode<Button>("BtnCargarPartida1");
		BtnCargarPartida2 = PnlJuego.GetNode<Button>("BtnCargarPartida2");
		LblCargarPartida = PnlJuego.GetNode<Label>("LblCargarPartida");
		LblNuevoJuego = PnlJuego.GetNode<Label>("LblNuevoJuego");

		PnlOpciones = GetNode<Panel>("PnlOpciones");
		BtnOpcion1 = PnlOpciones.GetNode<Button>("BtnOpcion1");
		BtnOpcion2 = PnlOpciones.GetNode<Button>("BtnOpcion2");
		LblOpciones = PnlOpciones.GetNode<Label>("LblOpciones");
		VolumeSlider = PnlOpciones.GetNode<HSlider>("VolumeSlider");
		GlowSlider = PnlOpciones.GetNode<HSlider>("GlowSlider");
		GlowCanvas = PnlOpciones.GetNode<CanvasModulate>("GlowCanvas");
		FadeRect = GetNode<ColorRect>("FadeRect");
		LblVolumen = PnlOpciones.GetNode<Label>("LblVolumen");
		LblBrillo = PnlOpciones.GetNode<Label>("LblBrillo");
		AudioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

		// Conectar eventos de botones
		BtnJugar.Pressed += PresionarJugar;
		BtnNuevoJuego.Pressed += PresionarNuevaPartida;
		BtnOpciones.Pressed += PresionarOpciones;
		BtnSalir.Pressed += PresionarSalir;
		BtnOpcion1.Pressed += _on_btn_opcion_1_pressed;
		BtnOpcion2.Pressed += _on_btn_opcion_2_pressed;
		BtnCargarPartida1.Pressed += _on_btn_cargar_partida_1_pressed;
		BtnCargarPartida2.Pressed += _on_btn_cargar_partida_2_pressed;
		VolumeSlider.ValueChanged += _on_VolumeSlider_value_changed;
		GlowSlider.ValueChanged += _on_glow_slider_value_changed;

		// Inicialización visual
		PnlOpciones.Visible = false;
		PnlJuego.Visible = false;
		FadeRect.Visible = false;
		FadeRect.Modulate = new Color(0, 0, 0, 0); // Transparente al inicio

		// Inicialización del slider de volumen y audio
		VolumeSlider.Value = 100;
		_on_VolumeSlider_value_changed(VolumeSlider.Value);
		GlowSlider.Value = 100;
		_on_glow_slider_value_changed(GlowSlider.Value);

		// Debug
		GD.Print("Inicialización completa del menú principal.");
	}
	
	private void PresionarJugar()
	{
		if(PnlJuego.Visible == false)
		{
			PnlJuego.Visible = true;
			PnlOpciones.Visible = false;
		}
		else
		{
			PnlJuego.Visible = false;
			PnlOpciones.Visible = false;
		}
	}
	
	//Metodo para abrir la escena principal del juego
	private void PresionarNuevaPartida()
	{
		FadeRect.Visible = true;
		// Creamos el tween para fundir a negro
		var tween = CreateTween();
		// Animamos el alpha del modulate de FadeRect de su valor actual a 1 (negro opaco), en 1 segundo
		tween.TweenProperty(FadeRect, "modulate:a", 1f, 1f);

		// Al terminar el tween, cambiamos la escena
		tween.TweenCallback(Callable.From(() =>
		{
			GetTree().ChangeSceneToFile("res://Main.tscn");
		}));
	}
	
	//Metodo para salir, si, es una boludez, podes simplemente asignarlo así: 'BtnSalir.Pressed += GetTree().Quit(); pero lo usamos de base para agregar otras cosas, como un cuadro de dialogo de confirmación
	private void PresionarSalir() => GetTree().Quit();
	
	private void PresionarOpciones()
	{
		if(PnlOpciones.Visible==false)
		{
			PnlOpciones.Visible = true; 
			PnlJuego.Visible = false;
		} 
		else 
		{
			PnlOpciones.Visible = false; 
			PnlJuego.Visible = false;
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
	
	private void _on_btn_cargar_partida_1_pressed() => PnlJuego.Visible = false;
	
	private void _on_btn_cargar_partida_2_pressed() => PnlJuego.Visible = false;
	
	private void _on_glow_slider_value_changed(double value)
	{
		float minDb=0.1f;
		float maxDb=1f;
		float GlowSliderValue = (float)value;
		float Brightness = (float)value;
		Brightness = Mathf.Lerp(minDb, maxDb, (float)Math.Log10(GlowSliderValue / 100f * 9 + 1));
		GlowCanvas.Color = new Color (Brightness, Brightness, Brightness, 1.0f);
		
	}
	
	
}
