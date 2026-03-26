# Informe Técnico: Solución al Problema de los Misioneros y Caníbales en C#

## 1. Descripción del Problema
El problema de los Misioneros y Caníbales es un clásico rompecabezas de cruce de ríos. Consiste en tres misioneros y tres caníbales que deben cruzar un río utilizando un bote que puede transportar a un máximo de dos personas a la vez.

**Restricciones:**
- El bote debe ser tripulado por al menos una persona.
- En ninguna de las dos orillas los caníbales pueden superar en número a los misioneros, ya que, de lo contrario, los caníbales se los comerían.
- El objetivo es mover a los seis individuos de la orilla izquierda a la orilla derecha siguiendo estas reglas.

## 2. Modelado del Estado
El estado del río se representó mediante la clase `RiverState` en la capa de **Domain**, utilizando los siguientes datos:
- `MissionariesLeft` (int): Cantidad de misioneros en la orilla izquierda (0 a 3).
- `CannibalsLeft` (int): Cantidad de caníbales en la orilla izquierda (0 a 3).
- `BoatIsLeft` (bool): Indica la posición del bote (true para orilla izquierda, false para derecha).

Los valores para la orilla derecha se calculan dinámicamente (`3 - valorIzquierda`). La lógica de validación asegura que en ambos lados se cumpla la restricción de seguridad (`misioneros >= caníbales` si `misioneros > 0`).

## 3. Representación del Nodo
Siguiendo los patrones de **Fred Mellender**, se implementó la interfaz `IGNode<T>`, la cual permite navegar por el árbol de búsqueda de forma abstracta. El objeto `MissionaryNode` contiene:
- `Data`: El estado actual del río (`RiverState`).
- `Parent`: Referencia al nodo padre, fundamental para reconstruir el camino una vez alcanzado el objetivo.
- `_visited`: Un conjunto (`HashSet`) compartido que rastrea estados ya procesados para evitar ciclos y bucles infinitos.
- `FirstChild()` / `NextSibling()`: Métodos para la navegación entre nodos sucesores, vinculando hermanos para facilitar el recorrido del grafo.

## 4. Algoritmo de Búsqueda Utilizado
Se utilizó **Búsqueda en Anchura (Breadth-First Search - BFS)** implementada a través del iterador `BreadthFirst()` en la clase genérica `Graph<T>`. 

**Justificación:**
- **Optimalidad:** BFS garantiza encontrar la solución con el número mínimo de movimientos (profundidad mínima).
- **Control de Ciclos:** Al utilizar un conjunto de estados visitados, el algoritmo ignora cualquier configuración que ya haya sido explorada, evitando el crecimiento infinito del árbol.
- **Implementación Mellender:** El recorrido se realiza extrayendo nodos de una cola y encolando sus hijos navegando a través de `firstChild()` y `nextSibling()`, separando la estructura del grafo del algoritmo de búsqueda.

## 5. Conclusión
La implementación exitosa demuestra cómo la aplicación de patrones de diseño específicos para la búsqueda permite resolver problemas complejos de forma modular y eficiente. El uso de una arquitectura de capas separa claramente la lógica del negocio (`Domain`), la lógica de búsqueda (`Application`) y la interacción con el usuario (`Infrastructure`).
