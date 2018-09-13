// JScript File

function HideElement(bol) {
    var frm = document.forms[0];
	for (var i=0; i<frm.elements.length; i++) {
	    if (bol == true)
	    {
	        if (document.forms[0].elements[i].tagName =="SELECT" || document.forms[0].elements[i].tagName == "OBJECT"){	           
	        if (frm.elements[i].style.visibilty == "hidden")
            {
                frm.elements[i].setAttribute("wasVisible",true);
            }
            else
            {
                if (frm.elements[i].getAttribute("wasVisible")!=null) frm.elements[i].setAttribute("wasVisible",false);
                frm.elements[i].style.visibility = "hidden";
            }	        
            }
	    }	    
	    else
	    {
	        
	        if (frm.elements[i].getAttribute("wasVisible")!=null)
	        {
	            if (frm.elements[i].getAttribute("wasVisible") == false)
	                frm.elements[i].style.visibility = "visible";
	        }
	        else
	        {
	            frm.elements[i].style.visibility = "visible";
	        }
	        
	    }		    
	}
}