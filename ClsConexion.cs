using Godot;
using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public partial class ClsConexion : Node
{
	private string CadenaConexion;

	public ClsConexion()
	{
		string DB_Path = ProjectSettings.GlobalizePath("res://DB_Game.db");
		CadenaConexion = $"Data Source={DB_Path}";
	}

	public SqliteConnection AbrirConexion()
	{
		var Conexion = new SqliteConnection(CadenaConexion);
		try
		{
			Conexion.Open();
			GD.Print("Conexión a la base de datos establecida correctamente.");
			return Conexion;
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Error al abrir la conexión: {ex.Message}");
			return null;
		}
	}
}
