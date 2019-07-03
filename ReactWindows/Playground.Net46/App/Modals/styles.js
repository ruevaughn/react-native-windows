import { StyleSheet } from 'react-native'

export default StyleSheet.create({
  modal: {
    backgroundColor: 'white',
    borderRadius: 4,
    width: 455,
    height: 270
  },
  wrapper: {
    position: 'absolute',
    top: 0,
    right: 0,
    bottom: 0,
    left: 0
  },
  backdrop: {
    position: 'absolute',
    top: 0,
    right: 0,
    bottom: 0,
    left: 0,
    backgroundColor: 'rgba(0,0,0,0.6)',
    justifyContent: 'center',
    alignItems: 'center'
  },
  modalWrapper: {
    position: 'absolute',
    top: 0,
    right: 0,
    bottom: 0,
    left: 0,
    justifyContent: 'center',
    alignItems: 'center'
  },
  titleWrapper: {
    height: 40,
    flexDirection: 'column',
    justifyContent: 'center'
  },
  title: {
    textAlign: 'center'
  },
  separator: {
    height: 1,
    width: 455,
    backgroundColor: 'gray'
  },
  content: {
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    flex: 1
  },
  imageBox: {
    height: 32,
    width: 46
  },
  version: {
    marginTop: 25
  },
  copyright: {
    marginTop: 25
  }
})
