using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundchecker : MonoBehaviour {
    public int isgrounded;
    
    // OM dett objekt nuddar ett objekt så händer detta nedan
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isgrounded ++;  // Kollar om du är på marken
    }
    // OM dett objekt lämnar ett objekt så händer detta nedan
    private void OnTriggerExit2D(Collider2D collision)
    {
        isgrounded --;  // Kollar om du inte är på marken
    }
   
}
