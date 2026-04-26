@startuml SewControl - Diagrama de Clases

skinparam classAttributeIconSize 0
skinparam classFontStyle Bold
skinparam classBackgroundColor #FCE4EC
skinparam classBorderColor #C2185B
skinparam classArrowColor #880E4F
skinparam classFontColor #212121
skinparam backgroundColor #FFFFFF

enum TipoEncargo {
    Confeccion
    Arreglo
    Bordado
    Otro
}

enum EstadoEncargo {
    Pendiente
    EnProceso
    Listo
    Entregado
    Cancelado
}

enum TipoPrenda {
    Camisa
    Pantalon
    Vestido
    Falda
    Chaqueta
    Traje
    Blusa
    Otro
}

enum TipoArreglo {
    Dobladillo
    Cremallera
    Zurcido
    Ajuste
    Otro
}

class Cliente {
    +int Id
    +string Nombre
    +string Apellido
    +string Telefono
    +string? Email
    +string? Direccion
    +DateTime FechaRegistro
    +bool IsDeleted
}

class Costurera {
    +int Id
    +string Nombre
    +string Apellido
    +string Telefono
    +string? Especialidad
    +bool Activa
    +bool IsDeleted
}

class Encargo {
    +int Id
    +TipoEncargo Tipo
    +EstadoEncargo Estado
    +DateTime FechaRecepcion
    +DateTime FechaEntregaEstimada
    +DateTime? FechaEntregaReal
    +decimal PrecioTotal
    +decimal? Anticipo
    +string? Observaciones
    +bool IsDeleted
    +int ClienteId
    +int CostureraId
}

class Prenda {
    +int Id
    +TipoPrenda Tipo
    +string Descripcion
    +string? Tela
    +string? Color
    +string? Talla
    +string? Observaciones
    +bool IsDeleted
    +int EncargoId
}

class Arreglo {
    +int Id
    +TipoArreglo Tipo
    +string Descripcion
    +decimal Costo
    +string? Observaciones
    +bool IsDeleted
    +int EncargoId
}

Cliente "1" --o{ "0..*" Encargo : tiene
Costurera "1" --o{ "0..*" Encargo : asignada a
Encargo "1" --o{ "0..*" Prenda : contiene
Encargo "1" --o{ "0..*" Arreglo : contiene
Encargo --> TipoEncargo
Encargo --> EstadoEncargo
Prenda --> TipoPrenda
Arreglo --> TipoArreglo

@enduml