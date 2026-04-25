
## Requerimientos Funcionales
#### 1. Gestión de Clientes

- **RF1:** El cliente debe poder registrarse con su nombre completo, número telefónico, correo y direcció
- **RF2:** El cliente debe poder modificar sus datos personales como teléfono, correo o dirección.
- **RF3:** El sistema debe mostrar el historial de encargos del cliente, incluyendo prendas, arreglos, fechas y montos pagados.

#### 2. Gestión de Encargos

- **RF4:** El sistema debe permitir crear encargos con tipo, fecha de entrega estimada, precio total, anticipo y observaciones.
- **RF5:** El sistema debe calcular automáticamente el saldo pendiente de cada encargo.
- **RF6:** El sistema debe gestionar los estados del encargo: Pendiente, EnProceso, Listo, Entregado y Cancelado.
- **RF7:** El sistema debe registrar automáticamente la fecha de entrega real al marcar un encargo como Entregado.
- **RF8**: El sistema debe permitir agregar prendas y arreglos detallados dentro de cada encargo.
- **RF9:** El sistema debe identificar y alertar sobre encargos atrasados automáticamente.

#### 3. Gestión de Costureras

- **RF10:** El sistema debe permitir registrar costureras con nombre, teléfono y especialidad.
- **RF11:** El sistema debe permitir activar o desactivar costureras según su disponibilidad.
- **RF12:** El sistema debe mostrar la carga de trabajo activa de cada costurera.
- **RF13:** El médico debe poder atender distintos tipos de encargo: Confección, Arreglo, Bordado y Otro.

## Requerimientos No Funcionales
#### 1. Seguridad

- **RNF1:** El sistema implementa soft delete en todas las entidades para preservar la integridad de los datos históricos sin eliminarlos físicamente de la base de datos.
- **RNF2:** El sistema implementa Global Query Filters en Entity Framework Core para excluir automáticamente los registros eliminados en todas las consultas.
- **RNF3:** El sistema implementa un middleware global de manejo de excepciones que captura todos los errores y devuelve siempre una respuesta estructurada mediante ApiResponse.
- **RNF4:** El sistema implementa CORS configurado en el backend para controlar qué orígenes pueden consumir la API.
- **RNF5:** El sistema implementa validaciones en el backend antes de guardar cualquier dato, evitando registros incompletos o inconsistentes.

#### 2. Rendimiento y Disponibilidad

- **RNF6:** El tiempo máximo de respuesta del sistema ante cualquier solicitud no debe superar los 5 segundos bajo condiciones normales.
- **RNF7:** Deben realizarse copias de seguridad de la base de datos de forma frecuente para garantizar la integridad de la información.

#### 3. Usabilidad y Accesibilidad

- **RNF8:** La interfaz del sistema debe ser intuitiva y fácil de usar para cualquier tipo de usuario, priorizando claridad visual y flujo natural de navegación.
- **RNF9:** La aplicación debe ser responsive, adaptándose correctamente a distintos dispositivos como móvil, tableta y escritorio.
  
