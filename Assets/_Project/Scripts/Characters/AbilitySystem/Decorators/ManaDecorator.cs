using System.Collections;
using MatchaIsSpent.CharactersStateSystem;
using UnityEngine;

namespace MatchaIsSpent.Characters.AbilitySystem
{
    /// <summary>
    /// This is a decorator class, it is used to add new functionalities to an ability.
    /// </summary>
    [CreateAssetMenu(fileName = "ManaDecorator", menuName = "MatchaIsSpent/AbilitySystem/Decorators/ManaDecorator")]
    public class ManaDecorator : BaseAbilitySO
    {
        [Tooltip("The mana cost of the ability.")]
        [SerializeField] private int manaCost = 10;
        [Tooltip("The ability to run.")]
        [SerializeField] private BaseAbilitySO decoredAbility;

        /// <summary>
        /// Constructor. As we use the decorator pattern base on Scriptable Objects, 
        /// we can't use the constructor to pass the ability to decorate.
        /// <paramref name="decoredAbility"/>: The ability to decorate.
        /// </summary>
        /// <param name="decoredAbility"></param>
        public ManaDecorator(BaseAbilitySO decoredAbility)
        {
            this.decoredAbility = decoredAbility;
        }

        /// <summary>
        /// Run the ability.
        /// <paramref name="controller"/>: The player controller.
        /// <paramref name="deltaTime"/>: The time since the last frame.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="deltaTime"></param>
        public override void Run(PlayerController controller, float deltaTime)
        {
            controller.ManaSystem.SpendMana(manaCost);
            decoredAbility.Run(controller, deltaTime);
        }
    }
}
