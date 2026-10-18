CREATE TABLE IF NOT EXISTS lectores (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    codigo TEXT NOT NULL,
    nombre TEXT NOT NULL,
    correo TEXT NOT NULL,
    telefono TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS libros (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    codigo TEXT NOT NULL,
    titulo TEXT NOT NULL,
    autor TEXT NOT NULL,
    ejemplares INTEGER NOT NULL,
    CHECK (ejemplares >= 0)
);

CREATE TABLE IF NOT EXISTS autores (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS prestamos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    lector_id INTEGER NOT NULL,
    libro_id INTEGER NOT NULL,
    fecha_prestamo TEXT NOT NULL,
    fecha_limite TEXT NOT NULL,
    FOREIGN KEY (lector_id) REFERENCES lectores (Id) ON DELETE CASCADE,
    FOREIGN KEY (libro_id) REFERENCES libros (Id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS idx_prestamos_lector 
ON prestamos (lector_id);