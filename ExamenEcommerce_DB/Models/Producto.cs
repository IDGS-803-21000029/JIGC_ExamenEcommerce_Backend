using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Producto
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Imagen { get; set; }
    public string Categoria { get; set; }
}
