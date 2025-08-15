using Godot;
using System;
using System.Collections.Generic;

public partial class ClsDialogo : Node
{
	public int iddialogo {get; set; }
	public string textodialogo {get; set; }
	
	public ClsDialogo(int ID_Dialogo, string TextoDialogo)
	{
		iddialogo = ID_Dialogo;
		textodialogo = TextoDialogo;
	}
}
