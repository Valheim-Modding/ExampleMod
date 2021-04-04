using System.Collections.Generic;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;

namespace ExampleMod.Pieces
{
    internal static class Example
    {
        internal static void Init()
        {
            ObjectDBHelper.OnAfterInit += AddPieceToTool;
            ObjectDBHelper.OnAfterInit += AddPieceFromPrefab;
        }

        private static void AddPieceToTool()
        {
            var hoePrefab = Prefab.Cache.GetPrefab<GameObject>("Hoe");
            var hoePieceTable = hoePrefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_buildPieces;

            var pieceChair = Mock<Piece>.Create("piece_chair");
            var clonedPrefab = Prefab.GetRealPrefabFromMock<Piece>(pieceChair).gameObject.InstantiateClone("customChair");

            var woodRequirement = MockRequirement.Create("Wood");
            woodRequirement.FixReferences();
            var customRequirements = new List<Piece.Requirement>
            {
                woodRequirement
            };

            var customPieceChair = clonedPrefab.GetComponent<Piece>();
            customPieceChair.m_resources = customRequirements.ToArray();
            customPieceChair.m_category = Piece.PieceCategory.Misc;

            hoePieceTable.m_pieces.Add(clonedPrefab.gameObject);
        }

        private static void AddPieceFromPrefab()
        {

        }

    }
}
