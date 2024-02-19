<%@ Page Title="" Language="C#" MasterPageFile="~/Student/classroomMasterPage.Master" AutoEventWireup="true" CodeBehind="viewAnnouncement.aspx.cs" Inherits="lms.Student.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/Student/viewAnnouncement.css" />
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <style>

    .emailname{
        display: none;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Student" runat="server">

     <div class="submit">
     <div class="class-content">
       <div class="details">
         <div class="title">

          <asp:Label ID="lbldate" runat="server" Text="" CssClass="h2"></asp:Label>

         </div>
         <div class="name">
             <asp:Label ID="lblteachername" runat="server" Text="" CssClass="h3"></asp:Label>
            <asp:Label ID="lblteacheremail" runat="server" CssClass="emailname" Text=""></asp:Label>

         </div>
       
        </div>
         <div class="modules">
            
<%--                 <p>please answer and submit within the day.  do not forget to turn in.</p>--%>
                   <asp:Label ID="lblpostcontent" runat="server" Text="" CssClass="p"></asp:Label>

         
         </div>
         <div class="comments">
             <div class="class-comments">
                <i class="fas fa-users"></i> 
                 <asp:Label ID="classCommentCountLabel" runat="server" Text='<%# Eval("CommentCount") %>' CssClass="lbl-comment"></asp:Label>
                  <span> Class comments </span>
             </div>
             <div class="comment-list">
            <asp:GridView ID="commentGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="" CssClass="announcement-grid">
       <Columns>
           <asp:TemplateField>
               <ItemTemplate>
                   <div class="announcement-box">
                       <div class="announcement-head">
                           <div class="profile">
                       <asp:Image ID="Image1" runat="server" CssClass="img-profile" ImageUrl='<%# GetProfileImageUrl(Eval("profileimage")) %>' />
                           </div>
                           <div class="info">
                               <div class="name">
                                   <h3><%# Eval("name") %></h3>                           
                                   <span><%# Eval("datepost") %></span>
                               </div>
                                <div class="announcement-body">
                                     <p><%# Eval("commentpost") %></p>
                                   </div>
                           </div>
                       </div>                     
                   </div>
               </ItemTemplate>
           </asp:TemplateField>
       </Columns>
   </asp:GridView>
<%--                 <asp:Label ID="lblUserId" runat="server" Text="Label"></asp:Label>--%>


             </div>
              <div class="announcement-comment">
                   <div class="post-div i">                                                  
                        <asp:Image ID="Image1" runat="server"  CssClass="img-profile"  />                 

                  </div>
                  <div class="post-div txt">
                      <asp:TextBox ID="txtcomment" runat="server" placeholder="Add comment" CssClass="txt-comment"></asp:TextBox>
                </div>
                  <div class="comment-button">
<%--                     <i class="fas fa-arrow-right"></i>--%>
                   <%--   <asp:Button ID="btncomment" runat="server" Text="→" OnClick="btncomment_Click" class="btn-comment"/>--%>
      
                      <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resources/submit2.png" CssClass="btn-comment"  OnClick="ImageButton1_Click" />
                      </div>

              </div>
         </div>
     </div>
  
     </div>


</asp:Content>
