# Hero's Way ⚔️

**Hero's Way** is a fast-paced Action RPG built in Unity that blends roguelike decision-making with deep character customization.

Battle your way through challenging levels and build your ultimate warrior. At the end of each stage, you choose one of three powerful **Upgrade Cards** to define your run. But survival isn't just about the cards you draw—it's about the gear you wield. Manage a full inventory, equip legendary loot, and permanently push your RPG stats to the limit to create unstoppable builds.

> **🕰️ Project Status: Legacy Portfolio Piece**
> *This is an older, uncompleted project. While I am no longer actively developing it, it remains in my portfolio to showcase my early systems architecture and my journey with Unity and C#.*

<img width="1920" height="1080" alt="gameplay+(1)" src="https://github.com/user-attachments/assets/eada1645-8548-470d-a2df-f7aed1f0a0b5" />

## 🎮 Core Gameplay Mechanics

- **Draft Your Destiny:** Choose from 3 unique upgrade cards at the end of every level to adapt your playstyle on the fly.
- **Deep Equipment System:** Loot, equip, and manage gear in your inventory to synergize with your card upgrades.
- **RPG Stat Customization:** Directly impact your combat performance by tweaking your Attack Damage, Attack Speed, Crit Chance, Crit Damage, Health, Armor, and Evasion.
- **Endless Replayability:** No two runs are exactly the same thanks to massive combinations of randomized cards and gear drops.

---

## ⚙️ Under the Hood (Technical Architecture)

This project was developed with a focus on scalable systems, clean data management, and Object-Oriented C# principles. 

### Inventory & Equipment System
* **Data-Driven Design:** All items and equipment are structured using `ScriptableObjects`. This completely decouples item data (stats, icons, descriptions) from the game logic, allowing new items to be created entirely within the Unity Editor.
* **Optimized Data Lookups:** The inventory relies on a dual-collection system. By pairing `List<InventoryItem>` with `Dictionary<ItemData, InventoryItem>`, the game maintains ordered UI lists while achieving $O(1)$ lookup times for stack management.
* **Dynamic Modifiers:** Equipping an item dynamically recalculates the player's base stats. The equipment class interfaces directly with the player's stats component to add or remove specific float modifiers on the fly.

<img width="1920" height="1080" alt="inventory_and_stats+(1)" src="https://github.com/user-attachments/assets/a002f1c3-fa3a-4475-996c-8613cc0dd690" />


### Stats & Combat Math
* **Robust Inheritance Hierarchy:** `CharacterStats.cs` acts as the base class containing universal logic (Health, Evasion calculations, Armor mitigation). `PlayerStats` and `EnemyStats` inherit from this base, keeping combat logic unified while allowing specific entities to handle custom behaviors.
* **Modular `Stat` Class:** Every RPG attribute is wrapped in a custom, serializable `Stat` class that independently manages its own base values, flat additive modifiers, and percentage multipliers. 
* **Event-Driven UI:** The UI uses C# delegates (`System.Action`) to listen for state changes (like taking damage or leveling up), eliminating the need for expensive `Update()` polling.

### Upgrades & Wave Management
* **Dynamic Skill Pooling:** The game controls the roguelike card system by maintaining separate lists for available and equipped abilities. When an upgrade is rolled, it is removed from the pool to prevent duplicate draws.
* **Weighted RNG Spawning:** The spawning system utilizes a cumulative probability algorithm, dynamically adjusting weights as the run progresses to introduce harder enemies over time.
* **Data-Driven Progression:** Both active and passive skills reference external JSON files to load scaling values and descriptions per level, keeping hardcoded numbers out of the logic scripts.

---

## 🚀 Quick Start & Controls

* **Start the Game:** After loading the main scene, press **[ F ]** to activate the spawner and start the initial wave!
* **Movement:**  WASD
* **Combat:** Left Click to Attack

---

## 🧠 Lessons Learned & Post-Mortem

As an older project, looking back at this codebase highlights several areas of growth in my software architecture journey. If I were to rebuild these systems today, I would implement the following optimizations:

* **Decoupling Logic from UI (Observer Pattern):** Instead of core managers forcing the UI to update, I would utilize C# Events to completely decouple backend logic from frontend visuals. 
* **Moving Away from Singletons:** The current architecture relies heavily on global Singletons (`Inventory.instance`, `PlayerManager.instance`). Today, I would use Dependency Injection (DI) to prevent tight coupling and make unit testing easier.
* **Object Pooling vs. Instantiation:** To prevent memory fragmentation and garbage collection spikes during dense waves, I would replace `Instantiate()` calls with an Object Pool pattern for enemies and projectiles.
* **Interfaces over `GetComponent`:** Replacing physics collision checks (like `GetComponent<Enemy>()`) with interfaces (e.g., `IDamageable`) would significantly improve performance during heavy combat scenarios.

---

## 🛠️ Built With

* **Engine:** Unity
* **Language:** C#

---
