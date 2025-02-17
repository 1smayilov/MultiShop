﻿namespace MultiShop.DtoLayer.Dtos
{
    public class ResultMessageDto
    {
        public int UserMessageId { get; set; }
        public string SenderId { get; set; }
        public string RecieverId { get; set; }
        public string Subject { get; set; }
        public string MessageDetail { get; set; }
        public bool IsRead { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
