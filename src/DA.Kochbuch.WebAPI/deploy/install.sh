#!/bin/bash
systemctl enable DA.Kochbuch.WebAPI.service
systemctl start DA.Kochbuch.WebAPI.service
systemctl status DA.Kochbuch.WebAPI.service
