<%@ Control Language="C#" %>

<table class="x-unselectable">
    <tr>
        <td>
            <table>
                <tr>
                    <td align='center'>
                        Available Cars
                    </td>
                <td align='center'>
                    Available Trucks
                </td>
                </tr>


                <tr>
                    <td>
                        <div id="cars" class="availableLot">
                            <div><img src="images/camaro.jpg" class="imgThumb" qtip="Camaro"/></div>
                            <div><img src="images/miata.jpg" class="imgThumb" qtip="Miata"/></div>
                            <div><img src="images/mustang.jpg" class="imgThumb" qtip="Mustang"/></div>
                            <div><img src="images/corvette.jpg" class="imgThumb" qtip="Corvette"/></div>
                        </div>
                    </td>

                        <td>
                            <div id="trucks" class="availableLot trucksLot">
                                <div><img src="images/f150.jpg" class="imgThumb"  qtip="F150"/></div>
                                <div><img src="images/tahoe.jpg" class="imgThumb" qtip="Tahoe"/></div>
                                <div><img src="images/tacoma.jpg" class="imgThumb" qtip="Tacoma"/></div>
                                <div><img src="images/s10.jpg" class="imgThumb" qtip="S10"/></div>
                            </div>
                        </td>
                    </tr>

            </table>
        </td>
        <td align='center' style="width: 200px;">
            <table>
                <tr>
                    <td align='center' style="width: 200px;">
                        Vehicles Rented
                    </td>
                    <td align='center' style="width: 200px;">
                        Vehicles In Repair
                    </td>
                </tr>
                <tr  style="">
                    <td style="">
                        <div id="rented" class="availableLot rented"></div>
                    </td>
                        <td>
                            <div id="repair" class="availableLot repair"></div>
                        </td>
                    </tr>

            </table>
        </td>
    </tr>
</table>   