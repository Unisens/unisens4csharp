
/**
 * XmlFileFormat is used to define that a XML representation of Entry data is used 
 * as file format. Using XML can lead to very big data files und slow access. Then 
 * BinFileFormat should be preferred. 
 * 
 * @author Joerg Ottenbacher
 * @author Radi Nedkov
 * @author Malte Kirst
 *
 */
namespace org.unisens
{
    public interface XmlFileFormat : FileFormat
    {

    }
}