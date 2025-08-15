using Godot;
using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public partial class ClsDialogos : Node
{
	private ClsConexion Conexion;

	public ClsDialogos()
	{
		Conexion = new ClsConexion();
	}

	public List<ClsDialogo> ObtenerDialogos()
	{
		var ListaDialogos = new List<ClsDialogo>();

		using (var conn = Conexion.AbrirConexion())
		{
			if (conn == null)
			{
				GD.PrintErr("No se pudo abrir la conexión");
				return ListaDialogos;
			}
			
			try
			{
				string query = "SELECT ID_Dialogo, TextoDialogo FROM Dialogos";
				using (var cmd = new SqliteCommand(query, conn))
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						int ID_Dialogo = reader.GetInt32(0); 
						string TextoDialogo = reader.GetString(1); 
						ListaDialogos.Add(new ClsDialogo(ID_Dialogo, TextoDialogo));
					}
				}
			}
			catch (Exception ex)
			{
				GD.PrintErr($"Error al obtener la lista de dialogos: {ex.Message}");
			}
		}
		return ListaDialogos;
	}
}
