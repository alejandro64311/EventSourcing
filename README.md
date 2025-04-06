# Event Sourcing Example in .NET Core

Este proyecto demuestra un **ejemplo sencillo** de cómo implementar Event Sourcing en ASP.NET Core, utilizando una clase `Pedido` (o `Order`) como agregado principal y un **almacenamiento de eventos** en memoria (para propósitos de ilustración).


---

## Descripción General

El proyecto ilustra:

1. **Cómo** modelar eventos de dominio (`PedidoCreado`, `ProductoAgregado`, `PedidoConfirmado`, etc.).
2. **Cómo** definir un **Agregado** que aplica dichos eventos para cambiar su estado.
3. **Cómo** implementar un **Event Store** básico (in-memory) y un **Repositorio** que:
   - Reconstruye el estado de un agregado a partir de los eventos guardados.
   - Persiste los eventos generados.
4. **Cómo** exponer endpoints de API para **crear un pedido**, **agregar productos**, **confirmar** y **consultar** su estado actual.


---

## Requisitos

- **.NET 6** o superior (SDK).
- Opcionalmente, **Visual Studio 2022** o **Visual Studio Code** para edición y depuración.
- Alguna herramienta para realizar llamadas HTTP (Postman, cURL, etc.).

---

## Configuración y Ejecución

1. **Clonar o descargar** este repositorio.
2. Abrir una terminal en la carpeta raíz del proyecto.
3. Ejecutar:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
