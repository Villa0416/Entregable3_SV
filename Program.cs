
public class Libro
{
    public Guid Id { get; set; }
    public string Título { get; set; }
    public string Resumen { get; set; }
    public Guid AutorId { get; set; }
}


public class Autor
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string Nacionalidad { get; set; }
}


var libros = new List<Libro>();
var autores = new List<Autor>();


app.MapPost("/api/v1/libros", (Libro libro) =>
{
    libro.Id = Guid.NewGuid();
    libros.Add(libro);
    return Results.Created($"/api/v1/libros/{libro.Id}", libro);
});

app.MapGet("/api/v1/libros/{id}", (Guid id) =>
{
    var libro = libros.FirstOrDefault(l => l.Id == id);
    if (libro == null)
        return Results.NotFound("El libro no fue encontrado.");
    return Results.Ok(libro);
});

app.MapPut("/api/v1/libros/{id}", (Guid id, Libro libro) =>
{
    var existingLibro = libros.FirstOrDefault(l => l.Id == id);
    if (existingLibro == null)
        return Results.NotFound("El libro no fue encontrado.");

    existingLibro.Título = libro.Título;
    existingLibro.Resumen = libro.Resumen;
    existingLibro.AutorId = libro.AutorId;

    return Results.Ok(existingLibro);
});

app.MapDelete("/api/v1/libros/{id}", (Guid id) =>
{
    var libro = libros.FirstOrDefault(l => l.Id == id);
    if (libro == null)
        return Results.NotFound("El libro no fue encontrado.");

    libros.Remove(libro);
    return Results.NoContent();
});


public class LibroDTO
{
    public string Título { get; set; }
    public string Resumen { get; set; }
    public Guid AutorId { get; set; }
}
