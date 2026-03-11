------------------------------------------------
-- 1. Modelado y creación de tablas ------------
------------------------------------------------

CREATE DATABASE EmpresaDB;

USE EmpresaDB;

CREATE TABLE Departamentos (
    DepartamentoId INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Empleados (
    EmpleadoId INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    DepartamentoID INT NOT NULL,
    FOREIGN KEY (DepartamentoId) REFERENCES Departamentos(DepartamentoId)
);

CREATE TABLE Proyectos (
    ProyectoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Presupuesto DECIMAL(18,2) NOT NULL
);

CREATE TABLE EmpleadoProyecto (
    EmpleadoId INT NOT NULL,
    ProyectoId INT NOT NULL,
    PRIMARY KEY (EmpleadoId, ProyectoId),
    FOREIGN KEY (EmpleadoId) REFERENCES Empleados(EmpleadoId),
    FOREIGN KEY (ProyectoId) REFERENCES Proyectos(ProyectoId)
);

INSERT INTO Departamentos (Nombre) VALUES
('Recursos Humanos'),
('Contabilidad'),
('Tecnología'),
('Administración'),
('Logística');

INSERT INTO Empleados (Nombre, Apellido, DepartamentoId) VALUES
('Maria', 'Pérez', 1),
('Luis', 'Gómez', 2),
('María', 'Rodríguez', 3),
('Carlos', 'Martínez', 4),
('Cristian', 'Correa', 5);

INSERT INTO Proyectos (Nombre, Presupuesto) VALUES
('Implementación ERP', 5000000.00),
('Campaña Publicitaria', 2000000.00),
('Migración a la nube', 7500000.00),
('Optimización logística', 3000000.00),
('Auditoría financiera', 1500000.00);

INSERT INTO EmpleadoProyecto (EmpleadoId, ProyectoId) VALUES
(1, 1),
(2, 5),
(3, 3),
(4, 2),
(5, 4),
(5, 1);

------------------------------------------------
-- 2.Consultas SQL -----------------------------
------------------------------------------------

SELECT e.EmpleadoId, (e.Nombre + ' ' + e.Apellido) As NombreEmpleado, d.Nombre AS Departamento
FROM Empleados e
INNER JOIN Departamentos d ON e.DepartamentoId = d.DepartamentoId;

SELECT p.ProyectoId, p.Nombre AS Proyecto, p.Presupuesto, COUNT(ep.EmpleadoId) AS CantidadEmpleados
FROM Proyectos p
LEFT JOIN EmpleadoProyecto ep ON p.ProyectoId = ep.ProyectoId
GROUP BY p.ProyectoId, p.Nombre, p.Presupuesto;

SELECT TOP 3 e.EmpleadoId, e.Nombre, e.Apellido, COUNT(ep.ProyectoId) AS ProyectosAsignados
FROM Empleados e
INNER JOIN EmpleadoProyecto ep ON e.EmpleadoId = ep.EmpleadoId
GROUP BY e.EmpleadoId, e.Nombre, e.Apellido
ORDER BY COUNT(ep.ProyectoId) DESC;

SELECT d.DepartamentoId, d.Nombre AS Departamento
FROM Departamentos d
LEFT JOIN Empleados e ON d.DepartamentoId = e.DepartamentoId
WHERE e.EmpleadoId IS NULL;

SELECT e.EmpleadoId, e.Nombre, e.Apellido, COUNT(ep.ProyectoId) AS ProyectosAsignados
FROM Empleados e
INNER JOIN EmpleadoProyecto ep ON e.EmpleadoId = ep.EmpleadoId
GROUP BY e.EmpleadoId, e.Nombre, e.Apellido
HAVING COUNT(ep.ProyectoId) > 1;

------------------------------------------------
-- 3. Procedimientos y funciones ---------------
------------------------------------------------

CREATE PROCEDURE sp_buscar_empleado
    @NombreEmpleado VARCHAR(100)
AS
BEGIN
    SELECT e.EmpleadoId,
           (e.Nombre + ' ' + e.Apellido) As NombreEmpleado,
           d.Nombre AS Departamento
    FROM Empleados e
    INNER JOIN Departamentos d ON e.DepartamentoId = d.DepartamentoId
    WHERE e.Nombre LIKE '%' + @NombreEmpleado + '%'
       OR e.Apellido LIKE '%' + @NombreEmpleado + '%';
END;

--EXEC sp_buscar_empleado @NombreEmpleado = 'Cris';

CREATE FUNCTION fn_total_proyectos (@IdEmpleado INT)
RETURNS INT
AS
BEGIN
    DECLARE @Total INT;

    SELECT @Total = COUNT(*)
    FROM EmpleadoProyecto
    WHERE EmpleadoId = @IdEmpleado;

    RETURN @Total;
END;

--SELECT dbo.fn_total_proyectos(1) AS TotalProyectosEmpleado;

------------------------------------------------
-- 4. Optimización y administración ------------
------------------------------------------------

CREATE NONCLUSTERED INDEX IX_Empleados_Apellido
ON Empleados (Apellido);

--Backup y Restore
--Se crea un backup en una ubicación física
BACKUP DATABASE EmpresaDB TO DISK = 'C:\Backups\EmpresaDB.bak';
--Query para restaurar la base de datos desde una ubicación física.
RESTORE DATABASE EmpresaDB FROM DISK = 'C:\Backups\EmpresaDB.bak' WITH REPLACE;

------------------------------------------------
-- 5. Bonus ------------------------------------
------------------------------------------------
--Se consume la api por navegador, se guarda el json y se consume la ruta física del json, 
--ya que sql server no me permitió hacer consultar por http/https

CREATE TABLE #UsuariosAPI (
    id INT,
    name NVARCHAR(200),
    username NVARCHAR(100),
    email NVARCHAR(200)
);

INSERT INTO #UsuariosAPI (id, name, username, email)
SELECT id, name, username, email
FROM OPENROWSET(
        BULK 'C:\Users\Usuario\OneDrive\Documentos\ProyectosDesarrollo\TalycalFullStack\BaseDeDatos\users.json',
        SINGLE_CLOB
    ) AS JsonData
CROSS APPLY OPENJSON(JsonData.BulkColumn)
WITH (
    id INT '$.id',
    name NVARCHAR(200) '$.name',
    username NVARCHAR(100) '$.username',
    email NVARCHAR(200) '$.email'
);

--select * from #UsuariosAPI
