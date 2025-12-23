# 🏗️ Bob The Builder - House Construction Game

**Semester 1 Project - Group 4**

A strategic construction simulation game where players manage resources, finances, and time to build their dream house within 30 days.

---

## 📋 About The Project

Bob The Builder is a text-based construction management game developed in C# using .NET 8.0. Players must strategically balance their budget, choose between different construction companies, and manage materials to complete a 16-phase house building project.

### Key Features

- 🏗️ **Three Construction Companies** with different quality/sustainability/price tiers
- 💰 **Dynamic Economy System** with banking, loans, and daily work opportunities
- 🌍 **Sustainability Mechanics** - material choices affect environmental impact
- 🎯 **16 Building Phases** covering foundation, flooring, walls, and roofing
- ⚡ **Natural Disasters** that can damage your construction based on material quality
- 📊 **Statistics Tracking** for money spent, sustainability score, and quality metrics
- 🗺️ **Interactive Map** to navigate between locations
- 🚗 **Fast Travel System** with purchasable car

---

## 🎮 How To Play

### Prerequisites
- .NET 8.0 SDK or later

### Running The Game

```bash
# Clone the repository
git clone https://github.com/yourusername/BobTheBuilder.git

# Navigate to project directory
cd BobTheBuilder/BobTheBuilder

# Run the game
dotnet run
```

### Running Tests

```bash
cd BobTheBuilder
dotnet test
```

---

## 🎯 Game Mechanics

### Construction Companies

| Company | Cost | Quality | Sustainability | Strategy |
|---------|------|---------|----------------|----------|
| **Big Build** | Low ($1,675) | Low | Low | Budget-friendly, fast completion |
| **Best Build** | Medium ($2,652) | Medium | Medium | Balanced approach |
| **Small Build** | High ($6,827) | High | High | Premium, requires loans or mixing |

### Economy

- **Starting Balance:** $3,000
- **Daily Work Income:** $800/day
- **Loans Available:** Up to $8,000 (with interest)
- **Time Limit:** 30 days maximum
- **Daily Choice:** Complete 1 quest OR work (not both)

### Building Phases

1. **Foundation (Phases 1-4)**: Excavation, footings, concrete slab, sealing
2. **Flooring (Phases 5-8)**: Joists, subfloor, flooring installation, finishing
3. **Walls (Phases 9-12)**: Framing, wall construction, insulation, exterior finish
4. **Roof (Phases 13-16)**: Trusses, sheathing, roofing material, final inspection

### Materials

Materials vary in price, sustainability, and quality:
- **Budget:** Gravel, Shingle, Insulation, Wood
- **Standard:** Bricks, Pine, Concrete
- **Premium:** EcoBlock, Recycled, Solar panels

---

## 🛠️ Technical Details

### Architecture

The project follows object-oriented principles with clear separation of concerns:

```
BobTheBuilder/
├── Data/           # Game initialization and quest definitions
├── Logic/          # Core game mechanics (Bank, Quest, House, etc.)
├── Presentation/   # Game loop and UI rendering
└── Tests/          # Unit tests for all components
```

### Technologies

- **Language:** C# 12
- **Framework:** .NET 8.0
- **Testing:** NUnit
- **Design Patterns:** Object-oriented design, encapsulation, inheritance

### Code Quality

- ✅ 54 unit tests (100% passing)
- ✅ Zero compiler warnings
- ✅ Comprehensive test coverage across all game systems

---

## 👥 Development Team

**Group 4 - Semester 1 Project**

- Filip
- Lautaro
- David
- Adam
- Alex
- Marius

---

## 🎓 Learning Outcomes

This project demonstrates understanding of:

- Object-Oriented Programming principles
- Game loop and state management
- Resource management systems
- Unit testing and test-driven development
- User interface design in console applications
- Financial simulation and economy balancing
- Strategic game design

---

## 📝 License

This project was created as part of an academic assignment.

---

## 🎉 Acknowledgments

Special thanks to our instructors for guidance throughout this project.
