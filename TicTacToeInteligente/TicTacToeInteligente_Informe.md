# Informe Técnico: Tic-Tac-Toe Inteligente con Patrones Mellender

Este documento detalla la implementación de un motor de Inteligencia Artificial para el juego Tic-Tac-Toe (Tres en Raya), utilizando el algoritmo Minimax y siguiendo los patrones de diseño propuestos por Fred Mellender en "Design Patterns for Searching in C#".

## 1. Descripción del Problema

El Tic-Tac-Toe es un juego de suma cero para dos jugadores (X y O) que se desarrolla en una cuadrícula de 3x3. El objetivo es ser el primero en colocar tres marcas en una línea horizontal, vertical o diagonal.

Desde la perspectiva de la Inteligencia Artificial, el Tic-Tac-Toe se modela como un **problema de búsqueda en un espacio de estados**. El árbol de juego para Tic-Tac-Toe tiene un factor de ramificación inicial de 9, que disminuye en cada nivel. El número total de estados posibles es relativamente pequeño (menos de 9!), lo que permite una búsqueda exhaustiva mediante el algoritmo Minimax.

## 2. Modelado del Estado

El estado del juego se representa en la clase `TicTacToeState`. 

- **Tablero:** Se utiliza un arreglo unidimensional de 9 posiciones (`Player[] Board`) para representar la cuadrícula de 3x3.
- **Jugador Actual:** Una propiedad que indica de quién es el turno (`X` o `O`).
- **Transiciones:** El método `MakeMove(int index)` genera un nuevo objeto `TicTacToeState` inmutable, lo cual facilita la exploración del árbol de búsqueda sin afectar el estado actual del juego.
- **Condición Terminal:** El método `IsTerminal()` verifica si hay un ganador o si el tablero está lleno (empate).

## 3. Representación del Nodo (Arquitectura Mellender)

Siguiendo la arquitectura de Fred Mellender, la navegación del árbol de búsqueda no se realiza mediante listas de hijos pre-calculadas, sino a través de una interfaz de navegación genérica `IGNode<T>`.

### Interfaz `IGNode<T>`
```csharp
public interface IGNode<T> {
    IGNode<T>? FirstChild();    // Obtiene el primer hijo (primer movimiento posible)
    IGNode<T>? NextSibling();   // Obtiene el hermano (siguiente movimiento posible al mismo nivel)
    IGNode<T>? Parent { get; }  // Referencia al nodo padre
    T State { get; }            // El estado del juego contenido en el nodo
}
```

### Clase `TicTacToeNode`
Esta clase implementa `IGNode<TicTacToeState>` de forma perezosa (lazy evaluation). 
- `FirstChild()`: Genera el primer estado posible a partir de los movimientos disponibles.
- `NextSibling()`: Utiliza la información del padre para generar el siguiente estado alternativo al mismo nivel de profundidad.
- **Movimiento Aplicado:** Cada nodo rastrea qué movimiento (`index`) se aplicó para llegar a ese estado, permitiendo reconstruir la mejor jugada.

## 4. Explicación del Algoritmo

### Minimax con Poda Alfa-Beta
El motor de búsqueda utiliza el algoritmo Minimax implementado en la clase `MinimaxEngine`. El objetivo es maximizar la puntuación para la computadora (O) y minimizarla para el humano (X).

1. **Propagación de Valores:**
   - **Victoria O:** +10 (ajustado por profundidad para preferir victorias rápidas).
   - **Victoria X:** -10 (ajustado por profundidad para retrasar derrotas).
   - **Empate:** 0.
2. **Búsqueda:** La clase `Graph<T>` proporciona métodos de utilidad para obtener todos los hijos de un nodo mediante la navegación de `FirstChild` y `NextSibling`.
3. **Poda Alfa-Beta:** Se ha implementado la poda Alfa-Beta para reducir el número de nodos evaluados, descartando ramas que no pueden influir en la decisión final.

### Clase `Graph<T>`
Actúa como el mediador para la búsqueda, encapsulando la lógica de expansión de nodos según el patrón Mellender, permitiendo que el algoritmo Minimax se mantenga desacoplado de la estructura específica del nodo.

## 5. Conclusión
La implementación demuestra cómo los patrones de diseño para búsqueda permiten separar la lógica del dominio (reglas del juego) de la infraestructura de búsqueda (Minimax y navegación del árbol). Gracias al algoritmo Minimax, la computadora garantiza un juego óptimo, logrando siempre ganar o empatar.
