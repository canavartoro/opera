<%@ Control Language="C#" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<ext:Window 
    runat="server"
    Title="Contact Us"
    Width="400"
    Height="400"
    MinWidth="300"
    MinHeight="300"
    DefaultFocus="firstName"
    Layout="FitLayout"
    Resizable="true"
    Modal="true">
    <Items>
        <ext:FormPanel 
            runat="server"
            Border="false"
            AutoDoLayout="true"
            BodyPadding="10">
            
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>

            <FieldDefaults 
                LabelAlign="Top"
                LabelWidth="100"
                LabelStyle="font-weight:bold;" />

            <Items>
                <ext:FieldContainer 
                    runat="server"
                    FieldLabel="Your Name"
                    LabelStyle="font-weight:bold;padding:0;"
                    Layout="HBoxLayout">

                    <FieldDefaults LabelAlign="Top" />

                    <Items>
                        <ext:TextField 
                            runat="server" 
                            Flex="1"
                            Name="firstName"
                            ItemID="firstName"
                            FieldLabel="First"
                            AllowBlank="false">
                            <AfterLabelTextTpl>
                                <Html>
                                    <span style="color:red;font-weight:bold" data-qtip="Required">*</span>
                                </Html>
                            </AfterLabelTextTpl>   
                         </ext:TextField>

                         <ext:TextField 
                            runat="server" 
                            Width="30"
                            Name="middleInitial"
                            FieldLabel="MI"
                            Margins="0 0 0 5" />

                         <ext:TextField 
                            runat="server" 
                            Flex="2"
                            Name="lastName"
                            FieldLabel="Last"
                            AllowBlank="false"
                            Margins="0 0 0 5">
                            <AfterLabelTextTpl>
                                <Html>
                                    <span style="color:red;font-weight:bold" data-qtip="Required">*</span>
                                </Html>
                            </AfterLabelTextTpl>   
                         </ext:TextField>
                    </Items>
                </ext:FieldContainer>

                <ext:TextField 
                    runat="server"
                    FieldLabel="Your Email Address"
                    Vtype="email"
                    AllowBlank="false">
                    <AfterLabelTextTpl>
                        <Html>
                            <span style="color:red;font-weight:bold" data-qtip="Required">*</span>
                        </Html>
                    </AfterLabelTextTpl>
                </ext:TextField>

                <ext:TextField 
                    runat="server"
                    FieldLabel="Subject"
                    AllowBlank="false">
                    <AfterLabelTextTpl>
                        <Html>
                            <span style="color:red;font-weight:bold" data-qtip="Required">*</span>
                        </Html>
                    </AfterLabelTextTpl>
                </ext:TextField>

                <ext:TextArea 
                    runat="server"
                    FieldLabel="Message"
                    LabelAlign="Top"
                    Flex="1"
                    Margins="0"
                    AllowBlank="false">
                    <AfterLabelTextTpl>
                        <Html>
                            <span style="color:red;font-weight:bold" data-qtip="Required">*</span>
                        </Html>
                    </AfterLabelTextTpl>
                </ext:TextArea>
            </Items>

            <Buttons>
                <ext:Button 
                    runat="server"
                    Text="Cancel">
                    <Listeners>
                        <Click Handler="this.up('form').getForm().reset(); this.up('window').hide();" />
                    </Listeners>
                </ext:Button>

                <ext:Button 
                    runat="server"
                    Text="Send"
                    Handler="sendForm" />
            </Buttons>

            <HtmlBin>
                <script>
                    function sendForm() {
                        if (this.up('form').getForm().isValid()) {
                            /* In a real application, this would submit the form to the configured url */
                            /* this.up('form').getForm().submit(); */
                            this.up('form').getForm().reset();
                            this.up('window').hide();
                            Ext.MessageBox.alert('Thank you!', 'Your inquiry has been sent. We will respond as soon as possible.');
                        }
                    }
                </script>
            </HtmlBin>
        </ext:FormPanel>
    </Items>
</ext:Window>