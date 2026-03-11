CREATE DATABASE TalycapGlobal;

USE TalycapGlobal;

CREATE TABLE TipoDocumento (
    id INT IDENTITY(1,1) PRIMARY KEY,
    tipoDocumento NVARCHAR(100) NOT NULL
);
 
INSERT INTO TipoDocumento (tipoDocumento)
VALUES 
    (N'Cédula de ciudadanía'),
    (N'Tarjeta de identidad'),
    (N'Otro');

CREATE TABLE Clientes (
    id INT IDENTITY(1,1) PRIMARY KEY,
    idTipoDocumento INT NOT NULL,
    numeroDocumento BIGINT NOT NULL,
    nombreCompleto NVARCHAR(200) NOT NULL,
    fechaNacimiento DATE NOT NULL,
    empresa NVARCHAR(150) NULL,
    activo BIT NOT NULL DEFAULT 1,
    fechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Clientes_TipoDocumento FOREIGN KEY (idTipoDocumento)
        REFERENCES TipoDocumento(id)
);


CREATE PROCEDURE sp_Clientes_Consultar
AS
BEGIN
    SELECT 
        id,
        idTipoDocumento,
        numeroDocumento,
        nombreCompleto,
        fechaNacimiento,
        empresa,
        activo,
        fechaCreacion
    FROM Clientes
    WHERE activo = 1;
END;
GO

CREATE PROCEDURE sp_Clientes_ConsultarPorId
    @id INT
AS
BEGIN
    SELECT 
        id,
        idTipoDocumento,
        numeroDocumento,
        nombreCompleto,
        fechaNacimiento,
        empresa,
        activo,
        fechaCreacion
    FROM Clientes
    WHERE activo = 1 AND id = @id;
END;
GO

CREATE PROCEDURE sp_Clientes_ConsultarPorNumeroDocumento
    @numeroDocumento BIGINT
AS
BEGIN
    SELECT 
        id,
        idTipoDocumento,
        numeroDocumento,
        nombreCompleto,
        fechaNacimiento,
        empresa,
        activo,
        fechaCreacion
    FROM Clientes
    WHERE activo = 1 AND numeroDocumento = @numeroDocumento;
END;
GO

CREATE PROCEDURE sp_Clientes_Insertar
    @idTipoDocumento INT,
    @numeroDocumento BIGINT,
    @nombreCompleto NVARCHAR(200),
    @fechaNacimiento DATE,
    @empresa NVARCHAR(150),
    @activo BIT = 1
AS
BEGIN
    INSERT INTO Clientes (
        idTipoDocumento,
        numeroDocumento,
        nombreCompleto,
        fechaNacimiento,
        empresa,
        activo,
        fechaCreacion
    )
    VALUES (
        @idTipoDocumento,
        @numeroDocumento,
        @nombreCompleto,
        @fechaNacimiento,
        @empresa,
        @activo,
        GETDATE()
    );
END;
GO

CREATE PROCEDURE sp_Clientes_Editar
    @id INT,
    @idTipoDocumento INT,
    @numeroDocumento BIGINT,
    @nombreCompleto NVARCHAR(200),
    @fechaNacimiento DATE,
    @empresa NVARCHAR(150),
    @activo BIT
AS
BEGIN
    UPDATE Clientes
    SET 
        idTipoDocumento = @idTipoDocumento,
        numeroDocumento = @numeroDocumento,
        nombreCompleto = @nombreCompleto,
        fechaNacimiento = @fechaNacimiento,
        empresa = @empresa,
        activo = @activo
    WHERE id = @id;
END;
GO

CREATE PROCEDURE sp_Clientes_Eliminar
    @id INT
AS
BEGIN
    UPDATE Clientes
    SET activo = 0
    WHERE id = @id;
END;
GO
