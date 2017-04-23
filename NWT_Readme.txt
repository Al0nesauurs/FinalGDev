Sunday March 23,

=======================================================================================

รายละเอียด ที่ push ไป

- Empty Object ชือ Network System
- ใน Asset/Prefab เพิ่ม Folder Network
- ที่ Player.prefab ใน Network เพิ่ม component network iden
- เพิ่ม OfflineCam
- แก้ code Playercontroller 
    - ในส่วน update บรรทัดแรกๆ 
    - หัวจาก MonoBehavior เป็น NetworkBehavior
    - เพิ่ม Library Network
    
=======================================================================================
  
รายละเอียดการทำงาน

- มีหน้าต่าง HUD สำหรับ เข้า Network
- ยังไม่ได้แก้พวกรายละเอียด เช่น ปืนหายพร้อม player เมื่อปิด network
- เลือดไม่ลด

=======================================================================================
