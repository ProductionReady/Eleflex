﻿@model $rootnamespace$.Models.ErrorViewModel
@{
    ViewBag.Title = "Error";
}
<h1>Error</h1>
<p>
    The page you have requested does not exist or an error has occurred in the system.
</p>
<div class="row">
    <div class="col-md-12">
        @if (Model != null && !string.IsNullOrEmpty(Model.Error))
        {
            <div class="alert alert-danger" role="alert">
                @Html.DisplayFor(x => x.Error)
            </div>
        }
    </div>
</div>
