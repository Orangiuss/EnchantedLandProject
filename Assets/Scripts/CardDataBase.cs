using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
	public static List<Card> cardList = new List<Card>();

	private void Awake()
	{
		// Carte "nulle" :
		cardList.Add(new Card(0, "", 0, 0, 0, "", Resources.Load<Sprite>("1"), 1, 1, false));

		// public Card(id, cardName, cost, power, pv, cardDescription, image, rarete, type, directAttack)
		
		/* rarete : 
		 * 0 : CARTES DE BASE
		 * 1 : COMMUNES
		 * 2 : RARES
		 * 3 : EPIQUE
		 * 4 : LEGENDAIRE
		*/

		/* type :
		 * 1 : HUMAIN
		 * 2 : ELFE
		 * 3 : NAIN
		 * 4 : BETE
		 * 5 : ORC
		 * 6 : AQUATIQUE
		 * 7 : AERIEN
		 */

		// Cartes de base (rarete 0) : 
			
		  //HUMAIN :

			cardList.Add(new Card(1, "Mercenaire perdu", 3, 3, 3, "", Resources.Load<Sprite>("MercenairePerdu"), 0, 1, false));
			cardList.Add(new Card(2, "Boucher", 3, 2, 2, "Inflige 2 points de dégat", Resources.Load<Sprite>("Boucher"), 0, 1, false, 0, 2));
			cardList.Add(new Card(3, "Joyeux chasseur", 7, 5, 6, "Détruit une bête ayant 2 PA ou moins", Resources.Load<Sprite>("JoyeuxChasseur"), 0, 1, false));

		  //ELFE :

			cardList.Add(new Card(4, "Joueur de la taverne", 3, 3, 2, "Donne +2 PV à un serviteur", Resources.Load<Sprite>("JoueurElfe"), 0, 2, false, 0,0,2));
			cardList.Add(new Card(5, "Druide elfique", 5, 4, 5, "Pioche 1 carte", Resources.Load<Sprite>("DruideElfe"), 0, 2, false, 1));
			cardList.Add(new Card(6, "Apprentie mage", 6, 2, 4, "Détruit un ennemi", Resources.Load<Sprite>("MageElfique"), 0, 2, false, 0, 100));

		  //NAIN :
			
			cardList.Add(new Card(7, "Massacreur nain", 5, 2, 2, "Détruit 1 elfe ennemi", Resources.Load<Sprite>("NainDeFeu"), 0, 3, false));
			cardList.Add(new Card(8, "Tireur précis", 5, 3, 3, "Inflige 3 points de dégat", Resources.Load<Sprite>("TireurNain"), 0, 3, false, 0, 3));
			cardList.Add(new Card(9, "Vieux sage", 6, 5, 7, "", Resources.Load<Sprite>("VieuxNain"), 0, 3, false));

		  //BETE :

			cardList.Add(new Card(10, "Rat des champs", 5, 3, 3, "Pioche 2 cartes", Resources.Load<Sprite>("RatDesChamps"), 0, 4, false, 2));
			cardList.Add(new Card(11, "Phoque furieux", 5, 5, 3, "Peut attaquer directement", Resources.Load<Sprite>("PhoqueFurieux"), 0, 4, true));
			cardList.Add(new Card(12, "Explorateur musicien", 2, 2, 3, "", Resources.Load<Sprite>("ExplorateurMusicien"), 0, 4, false));

		  //ORC :

			cardList.Add(new Card(13, "Pilleur orc", 1, 1, 2, "Pioche 1 carte", Resources.Load<Sprite>("PilleurOrc"), 0, 5, false, 1));
			cardList.Add(new Card(14, "Archer orc", 2, 1, 2, "Inflige 2 points de dégat", Resources.Load<Sprite>("ArcherOrc"), 0, 5, false, 0, 2));
			cardList.Add(new Card(15, "Combattante orc", 6, 4, 10, "", Resources.Load<Sprite>("CombattanteOrc"), 0, 5, false));

		  //AQUATIQUE :

			cardList.Add(new Card(16, "Poisson fragile", 1, 2, 2, "", Resources.Load<Sprite>("PoissonFragile"), 0, 6, false));
			cardList.Add(new Card(17, "Poisson angélique", 4, 3, 5, "", Resources.Load<Sprite>("PoissonAngelique"), 0, 6, false));
			cardList.Add(new Card(18, "Monstruosité des profondeurs", 8, 6, 10, "", Resources.Load<Sprite>("MonstruositeDesProfondeurs"), 0, 6, false));

		  //AERIEN :

			cardList.Add(new Card(19, "Griffon", 5, 5, 6, "", Resources.Load<Sprite>("Griffon"), 0, 7, false));
			cardList.Add(new Card(20, "Dragon sylvestre", 7, 7, 3, "Peut attaquer directement", Resources.Load<Sprite>("DragonSylvestre"), 0, 7, true));
			cardList.Add(new Card(21, "Terreur volante", 3, 2, 5, "Donne +1 PV à un serviteur", Resources.Load<Sprite>("TerreurVolante"), 0, 7, false,0,0,1));

		// Cartes communes (rarete 1) :

			//HUMAIN
			// ELFE

			cardList.Add(new Card(22, "Amie des dragons", 2, 2, 3, "Inflige 3 points de dégats si vous controlez un aérien", Resources.Load<Sprite>("AmieDesDragons"), 1, 2, false, 0,3,0));

			// NAIN
			// BETE
			// ORC

			cardList.Add(new Card(23, "Tueur orc", 4, 3, 4, "Inflige 1 point de dégats à tous les serviteurs ennemis", Resources.Load<Sprite>("TueurOrc"), 1, 5, false));

			// AQUATIQUE
			// AERIEN

		// Cartes rares (rarete 2) : 

			//HUMAIN
			// ELFE
			// NAIN
			// BETE
			// ORC
			// AQUATIQUE
			// AERIEN

		// Cartes épiques (rarete 3) :

			//HUMAIN
			// ELFE

			cardList.Add(new Card(24, "Enfant enchanteur", 5, 2, 2, "Donne +1 d'attaque et +2 de vie à tous vos elfes", Resources.Load<Sprite>("EnfantEnchanteur"), 3, 2, false));

			// NAIN

			cardList.Add(new Card(25, "Reine naine", 3, 2, 2, "Donne +2 d'attaques à tous vos nains", Resources.Load<Sprite>("ReineNaine"), 3, 3, false));

			// BETE
			// ORC

			cardList.Add(new Card(26, "Demoniste gobelin", 4, 3, 3, "Invoque 2 démons orcs qui peuvent attaquer directement", Resources.Load<Sprite>("DemonisteOrc"), 3, 5, false));
			cardList.Add(new Card(27, "Demon orc", 1, 1, 1, "Peut attaquer directement", Resources.Load<Sprite>("DemonOrc"), 0, 5, true));

			// AQUATIQUE
			// AERIEN

		// Cartes légendaires (rarete 4) : 

			//HUMAIN
			// ELFE

			cardList.Add(new Card(28, "Roi des ténèbres", 10, 9, 10, "Invoque 3 orcs aléatoires", Resources.Load<Sprite>("RoiDesTenebres"), 4, 2, false));

			// NAIN

			cardList.Add(new Card(29, "Radluir double-hache", 7, 7, 7, "Inflige 5 points de dégat si vous avez 2 autres nains", Resources.Load<Sprite>("DoubleHache"), 4, 3, false,0,5));

			// BETE

			cardList.Add(new Card(30, "Iris, combattante renarde", 12, 7, 7, "Coûte 1 énergie de moins par bêtes alliés, peut attaquer directement", Resources.Load<Sprite>("Iris"), 4, 4, true));

			// ORC
			// AQUATIQUE

			cardList.Add(new Card(31, "Kraken", 5, 4, 7, "+2 PA si vous avez 2 autres serviteurs aquatiques", Resources.Load<Sprite>("Kraken"), 4, 6, false));

			// AERIEN
	}
}
