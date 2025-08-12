using Godot;
using System;

public partial class HUD : Control //=> Clase pública declarada. Heredamos la clase de Control
{
	//Tenemos tres variables con '[Export]', así que en el inspector de UI podemos cambiar estás tres variables.
	[Export] public NodePath player_path; //=> Hacemos una "conexión" con la clase Player, a través del inspector
	[Export] private NodePath ControlesJuego_path;//=> Hacemos lo mismo pero con el Panel de ControlesJuego
	[Export] private NodePath PanelDialogo_path;//=> Hacemos lo mismo pero con el Panel de PanelDialogo
	
	//Declaramos los controladores gráficos y nodos principales (menos UI) como variables de manera privada.
	private Player Jugador; //=> Player, el nodo principal 
	private Panel ControlesJuego, PanelDialogo; //=> Los dos paneles (sirven como contenedores)
	private ProgressBar BarradeHambre, BarradeRelacion;//=> La barra de progreso
	private Label LblInfo, LblCharla;//=> Los dos labels. Podemos ponerlos así para evitar muchas lineas de código
	private Button BtnHablar, BtnExplorar, BtnJugar; //=> Los tres botones. Misma lógica que la de los labels
	private Timer HambreTemporizador;//=> El temporizador
	
	
	//Inicializamos los controladores especificando su ubicación en el nodo
	public override void _Ready()
	{
		//A cada variable le asignamos su tipo de controlador entre '<>' seguidos de su ubicación. Los nodos principales son su propia variable, como 'Player', 'Control', etc.
		Jugador = GetNode<Player>(player_path);//=> En el inspector ya declaramos su ubicación.
		ControlesJuego = GetNode<Panel>(ControlesJuego_path);//=> Lo mismo acá
		PanelDialogo = GetNode<Panel>(PanelDialogo_path);//=> Lo mismo acá
		
		/*Las siguientes ubicaciones se dan según su posición en el nodo UI (este no se especifica)
		Si estuvieran sueltas en el nodo UI, sin estar en ninguna ubicación seria (LblEjemplo)
		Pero como están en un contenedor primero se pone este y luego el controlador => (Container/Label)
		Si hubiera más de un container => (Container2/Container1/Label), y así...
		*/
		BarradeHambre = GetNode<ProgressBar>("ControlesJuego/BarradeHambre"); 
		LblInfo = GetNode<Label>("ControlesJuego/LblInfo");
		BtnHablar = GetNode<Button>("ControlesJuego/BtnHablar");
		BtnExplorar = GetNode<Button>("ControlesJuego/BtnExplorar");
		BtnJugar = GetNode<Button>("ControlesJuego/BtnJugar");
		LblCharla = GetNode<Label>("PanelDialogo/LblCharla");
		BarradeRelacion = GetNode<ProgressBar>("ControlesJuego/BarradeRelacion");
		
		/*Inicializamos la lógica de '¿Qué pasa?' cuando se llama a una función integrada de un controlador gráfico
		En este caso .Pressed del controlador gráfico de los buttons. Hay que hacerlo para cada uno de los que están
		'+=' es como un 'agregar', básicamente 'redirige'.
		*/ 
		BtnHablar.Pressed += PresionarHablar; //=> Cuándo se presiona se llama la función "PresionarHablar"
		BtnExplorar.Pressed += PresionarExplorar;//=> Se llama la función PresionarExplorar
		BtnJugar.Pressed += PresionarJugar;//=> Se llama la función PresionarJugar
		
		//Inicializamos las funciones incorporadas del temporizador
		HambreTemporizador = GetNode<Timer>("../HambreTemporizador"); //=> Asiganamos su nodepath, No está dentro de UI, es su nodo hermano, así que va con (../) al principio
		HambreTemporizador.WaitTime = 5.0; //=> .WaitTime es cada cuanto deja de contar
		HambreTemporizador.Autostart = true; //=> .Autostart indica que se inicializa cuando se abre la escena o deja de contar
		HambreTemporizador.Timeout += HambrePasivo;//=> Cuando deja de contar y que acción hace, en este caso devuelve una función que resta hambre
		
		//Funciones para que el botón de hablar haga más 'estético' el juego
		PanelDialogo.GuiInput += ClickMousePaneldeDialogo; //=> Al PanelDialogo le llamamos la función incorporada de .GuiInput la función nombrada, que es del click del mouse. .GuiInput permite trabajar con el input del usuario (perifericos)
		PanelDialogo.Visible = false; //=> Inicializamos al PanelDialogo con visibilidad false, ya que solo se mostrará si se habla
	ActualizarUI(); //=> Función que actualiza los datos del hambre según lo que suceda
	}
	
	//Funcion que se llama cuando se aprieta el botón de hablar
	private void PresionarHablar() 
	{
		ControlesJuego.Visible = false; //=> La visibilidad de los controladores gráficos se pasa a false (era true)
		PanelDialogo.Visible = true; //=> La visibilidad de los controladores gráficos del dialogo pasa a true (era false)
		
		string texto = Jugador.Hablar(); //=> Generamos una variable string que tendrá el dialogo que genere la función de Jugador.Hablar()
		//PD = 'Jugador' incluye toda la clase del Nodo 'Player'. Podes acceder a sus métodos y funciones, las de Player.cs por qué lo declaraste al inicio, en el _Ready()
		LblCharla.Text = texto; //=> Cambiamos el texto del label del PanelDialogo al dialogo que se generó.
		//PD = Para cambiar el texto de un label se tiene que hacer referencia al .Text del label y no al label en si, no hagan 'LblCharla = Text', ya que es un error
		ActualizarUI();
	}
	
	//Función que se llama cuando se aprieta el botón de explorar
	private void PresionarExplorar()
	{
		//var es una variable que toma un tipo de dato según la situación, es cómo un 'comodin', le podes asignar cualquier tipo de dato.
		var(exito, cantidad) = Jugador.Explorar(); //=> esta variable tendrá 'exito' y 'cantidad'. Explorar() devuelve un bool y un int respectivamente, de ahí a que creemos este var con dos variables y con esos nombres (exito = true o false, cantidad = entero) 
		if (exito) //=> if (exito == true). Si no se le pone nada a un booleano en un if es por qué preguntamos si es true
		{
			//En un string, si antes de la primera comilla ponemos '$' nos permite ponerle variables dentro, siempre entre '{}'.
			LblInfo.Text = $"¡Éxito, haz encontrado {cantidad} de comida!";
		}
		else //=> Si no es true...
		{
			LblInfo.Text = "No encontraste nada jijo...";
		}
		ActualizarUI(); //=> Actualiza el hambre. La cantidad si es reducida del hambre
	}
	
	//Función que se llama cuando se aprieta el botón de jugar
	private void PresionarJugar()
	{
		LblInfo.Text = Jugador.Jugar(); //=> .Jugar() devuelve un string, entonces se lo podemos asignar a un .Text
		ActualizarUI(); //=> Actualizamos con el hambre generada por jugar
	}
	
	//Mécanica que hace que a medida que pase el tiempo se reduzca el hambre. PD = Si se reduce el hambre es 'malo'
	private void HambrePasivo()
	{
		Jugador.Hambre = Math.Max(0, Jugador.Hambre - 1); //Restamos una unidad de hambre cuando se llame. (En el temporizador, en el _Ready() de esta clase)
		//A la instancia de Jugador (Player.cs) no solo podemos llamar sus funciones, si no también que a sus variables 'globales' también
		ActualizarUI(); //=> Actualizamos
	}
	
	//Método que actualiza los valores del hambre del temporizador según los cambios que se hagan
	private void ActualizarUI()
	{
		BarradeHambre.Value = Jugador.Hambre; //Al value de la barra del hambre le asignamos nuestro apetito 
		//BarradeCordura.Value = Jugador.Cordura; etc...
		BarradeRelacion.Value = Jugador.Relacion;
	}
	
	//Método que se llama cuando el jugador aprieta click con el mouse en el panel de dialogo (el LblCharla del PanelDialogo)
	private void ClickMousePaneldeDialogo(InputEvent @event) //InputEvent permite almacenar variables de eventos relacionados al gui (perifericos)
	{
		if (@event is InputEventMouseButton MouseClick && MouseClick.Pressed) //=> traducido = 'Si el evento es un click de mouse (le ponemos un nombre) y este es presionado...' (suena down, lo se)
		{
			PanelDialogo.Visible = false; //El PanelDialogo cambia su visibilidad a false, cerrandose
			ControlesJuego.Visible = true; //Los controladores del juego (botones y demás) vuelven a aparecer
			
		}
	}
	
	//Cualquier duda me preguntan.
}
