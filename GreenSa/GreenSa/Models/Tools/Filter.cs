using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.Tools
{
    /**
     * Permet de filter des éléments
     * Il s'agit juste d'un "type" qui prend un élément de type E et qui retourne un boolean
     * exemple avec une lambda : 
     * Filter<GolfCourse>.Filtre f = (c => c.attribute == 2);
     * ensuite on l'applique avec f(E)
     * */
    public class Filter<E>  
    {
        public delegate bool Filtre(E element);
    }
}
